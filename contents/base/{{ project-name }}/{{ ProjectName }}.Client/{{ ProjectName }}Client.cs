using Grpc.Net.Client;
using {{ ProjectName }}.API;

namespace {{ ProjectName }}.Client;

public class {{ ProjectName }}Client : I{{ ProjectName }}
{
    private readonly API.{{ ProjectName }}.{{ ProjectName }}Client stub;

    private {{ ProjectName }}Client(GrpcChannel channel)
    {
        stub = new API.{{ ProjectName }}.{{ ProjectName }}Client(channel);
    }

    public static {{ ProjectName }}Client Of(string host)
    {
        return new {{ ProjectName }}Client(GrpcChannel.ForAddress(host));
    }

{%- for entity_key in model.entities -%}
{%- set EntityName = entity_key | pascal_case -%}
{%- set entityName = entity_key | camel_case %}

    public async Task<Create{{ EntityName }}Response> Create{{ EntityName }}({{ EntityName }}Dto {{ entityName }}) {
        return await stub.Create{{ EntityName }}Async({{ entityName }});
    }

    public async Task<Get{{ EntityName | pluralize }}Response> Get{{ EntityName | pluralize }}(Get{{ EntityName | pluralize }}Request request) {
        return await stub.Get{{ EntityName | pluralize }}Async(request);
    }

    public async Task<Get{{ EntityName }}Response> Get{{ EntityName }}(Get{{ EntityName }}Request request) {
        return await stub.Get{{ EntityName }}Async(request);
    }

    public async Task<Update{{ EntityName }}Response> Update{{ EntityName }}({{ EntityName }}Dto {{ entityName }}) {
        return await stub.Update{{ EntityName }}Async({{ entityName }});
    }

    public async Task<Delete{{ EntityName }}Response> Delete{{ EntityName }}(Delete{{ EntityName }}Request request) {
        return await stub.Delete{{ EntityName }}Async(request);
    }
    
{% endfor %}
}
