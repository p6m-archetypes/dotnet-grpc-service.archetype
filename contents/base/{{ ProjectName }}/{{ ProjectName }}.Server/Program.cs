using Microsoft.EntityFrameworkCore;
using {{ ProjectName }}.Core;
using {{ ProjectName }}.Persistence.Context;
using {{ ProjectName }}.Persistence.Repositories;
using {{ ProjectName }}.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddScoped<{{ ProjectName }}Core>();


//Configure Repositories and DBContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CockroachDBConnection")));

{%- for entity_key in model.entities -%}
{%- set EntityName = entity_key | pascal_case -%}
{%- set entityName = entity_key | camel_case %}
builder.Services.AddScoped<{{ EntityName }}Repository, {{ EntityName }}Repository>();
{% endfor %}


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcReflectionService().AllowAnonymous();
app.MapGrpcService<{{ ProjectName }}GrpcImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
