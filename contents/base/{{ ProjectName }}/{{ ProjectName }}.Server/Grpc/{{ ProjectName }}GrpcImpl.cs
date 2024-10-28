using Grpc.Core;
using {{ ProjectName }}.API;
using {{ ProjectName }}.API.Logger;
using {{ ProjectName }}.Core;

using ILogger = Serilog.ILogger;

namespace {{ ProjectName }}.Server.Grpc;

public class {{ ProjectName }}GrpcImpl : API.{{ ProjectName }}.{{ ProjectName }}Base
{
    private static readonly ILogger Logger = LogFactory.CreateLogger(typeof({{ ProjectName }}GrpcImpl).ToString());
    private readonly {{ ProjectName }}Core _service;
    public {{ ProjectName }}GrpcImpl({{ ProjectName }}Core service)
    {
        _service = service;
    }

{%- for entity_key in model.entities -%}
{%- set EntityName = entity_key | pascal_case -%}
{%- set entityName = entity_key | camel_case %}

    public override Task<Create{{ EntityName }}Response> Create{{ EntityName }}({{ EntityName }}Dto request, ServerCallContext context)
    {
        Logger.Information("Create{{ EntityName }} request: {@request}", request);
        return _service.Create{{ EntityName }}(request);
    }

    public override Task<Get{{ EntityName }}Response> Get{{ EntityName }}(Get{{ EntityName }}Request request, ServerCallContext context)
    {
        Logger.Information("Get{{ EntityName }} request: {@request}", request);
        return _service.Get{{ EntityName }}(request);
    }

    public override Task<Get{{ EntityName | pluralize }}Response> Get{{ EntityName | pluralize }}(Get{{ EntityName | pluralize }}Request request, ServerCallContext context)
    {
        Logger.Information("Get{{ EntityName | pluralize }} request: {@request}", request);
        return _service.Get{{ EntityName | pluralize}}(request);
    }

    public override Task<Update{{ EntityName }}Response> Update{{ EntityName }}({{ EntityName }}Dto request, ServerCallContext context)
    {
        Logger.Information("Update{{ EntityName }} request: {@request}", request);
        return _service.Update{{ EntityName }}(request);
    }
{% endfor %}
}
