using System.Text.Json;
{% if persistence != 'None' %}using Microsoft.EntityFrameworkCore;{% endif %}
using {{ ProjectName }}.Core;
using Testcontainers.CockroachDb;
{% if persistence != 'None' %}
using {{ ProjectName }}.Persistence.Context;
using {{ ProjectName }}.Persistence.Repositories;{% endif %}
using {{ ProjectName }}.Server.Grpc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


namespace {{ ProjectName }}.Server;

public class Startup
{
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }
    public IConfigurationRoot Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddGrpc();
        services.AddGrpcReflection();

        services.AddScoped<{{ ProjectName }}Core>();

        {% if persistence != 'None' %}
        //Configure Repositories and DBContext
        bool isTempDb = bool.Parse(Configuration["TEMP_DB"] ?? "false");

        string? connectionString;
        if (isTempDb)
        {
            // Start temporary DB
            CockroachDbContainer cockroachDbContainer = new CockroachDbBuilder()
                .WithImage("cockroachdb/cockroach:v22.1.0")
                .WithPortBinding(26257, true)
                .Build();

            cockroachDbContainer.StartAsync().Wait();
            connectionString = cockroachDbContainer.GetConnectionString();
        }
        else
        {
            connectionString = Configuration.GetConnectionString("CockroachDBConnection");
        }
        
        //Configure Repositories and DBContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        {%- for entity_key in model.entities -%}
        {%- set EntityName = entity_key | pascal_case -%}
        {%- set entityName = entity_key | camel_case %}
        services.AddScoped<{{ EntityName }}Repository, {{ EntityName }}Repository>();
        {% endfor %}{% endif %}
        
        services.AddHealthChecks(){% if persistence != 'None' %}
            .AddNpgSql(connectionString,
                name: "cockroachdb",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["db", "cockroachdb", "sql"]
            ){% endif %};

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("order-service"))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
                metrics.AddOtlpExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddGrpcCoreInstrumentation()
                    .AddOtlpExporter();
            })
            ;
    }
        
        
    public void Configure(WebApplication app)
    {
        //Run DB Migrations
        using (var scope = app.Services.CreateScope())
        {
            var servicesProvider = scope.ServiceProvider;
            var context = servicesProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();  // Apply pending migrations
        }

        // Configure the HTTP request pipeline.
        app.MapGrpcReflectionService().AllowAnonymous();
        app.MapGrpcService<{{ ProjectName }}GrpcImpl>();
        app.MapGet("/", () => "{{ ProjectName }}");

        app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description ?? "No description",
                        duration = e.Value.Duration.ToString()
                    })
                });
                await context.Response.WriteAsync(result);
            }
        });
    }
}