{% import "macros/dotnet" as dotnet %}
namespace {{ ProjectName }}.API;

public interface I{{ ProjectName }}
{
    {%- for entity_key in model.entities %}
    {{ dotnet.api_interface_methods(entity_key, model.entities[entity_key], model)}}
    {% endfor %}
}
