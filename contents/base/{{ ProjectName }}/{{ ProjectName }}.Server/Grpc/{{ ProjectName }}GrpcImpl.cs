using Grpc.Core;
using {{ ProjectName }}.API;
using {{ ProjectName }}.Core;

namespace {{ ProjectName }}.Server.Services;

public class {{ ProjectName }}GrpcImpl : API.{{ ProjectName }}.{{ ProjectName }}Base
{
    private readonly ILogger<{{ ProjectName }}GrpcImpl> _logger;
    private readonly {{ ProjectName }}Core _service;
    public {{ ProjectName }}GrpcImpl(ILogger<{{ ProjectName }}GrpcImpl> logger, {{ ProjectName }}Core service)
    {
        _logger = logger;
        _service = service;
    }

{%- for entity_key in model.entities -%}
{%- set EntityName = entity_key | pascal_case -%}
{%- set entityName = entity_key | camel_case %}

    public override Task<Create{{ EntityName }}Response> Create{{ EntityName }}({{ EntityName }}Dto request, ServerCallContext context)
    {
        _logger.LogInformation("Create{{ EntityName }} request: {@request}", request);
        return _service.Create{{ EntityName }}(request);
    }

    public override Task<Get{{ EntityName }}Response> Get{{ EntityName }}(Get{{ EntityName }}Request request, ServerCallContext context)
    {
        _logger.LogInformation("Get{{ EntityName }} request: {@request}", request);
        return _service.Get{{ EntityName }}(request);
    }

    public override Task<Get{{ EntityName | pluralize }}Response> Get{{ EntityName | pluralize }}(Get{{ EntityName | pluralize }}Request request, ServerCallContext context)
    {
        _logger.LogInformation("Get{{ EntityName | pluralize }} request: {@request}", request);
        return _service.Get{{ EntityName | pluralize}}(request);
    }

    public override Task<Update{{ EntityName }}Response> Update{{ EntityName }}({{ EntityName }}Dto request, ServerCallContext context)
    {
        _logger.LogInformation("Update{{ EntityName }} request: {@request}", request);
        return _service.Update{{ EntityName }}(request);
    }
{% endfor %}
}
