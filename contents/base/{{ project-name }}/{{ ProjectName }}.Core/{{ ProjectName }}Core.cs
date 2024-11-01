{% import "macros/dotnet" as dotnet %}
using Grpc.Core;
using {{ ProjectName }}.API;
{% if persistence != 'None' %}
{{ dotnet.core_persistence_imports()}} {% endif %}

namespace {{ ProjectName }}.Core;

public class {{ ProjectName }}Core : I{{ ProjectName }}
{
    {{ dotnet.core_persistence_fields() }}   
    public {{ ProjectPrefix }}{{ ProjectSuffix }}Core({{ dotnet.core_persistence_constructor_args() }}) 
    {{'{'}}
        {{ dotnet.core_persistence_constructor_assignments() }}
    }

    {%- for entity_key in model.entities %}
    {{ dotnet.core_implementation_methods(entity_key, model.entities[entity_key], model) }}
    {% endfor %}

}
