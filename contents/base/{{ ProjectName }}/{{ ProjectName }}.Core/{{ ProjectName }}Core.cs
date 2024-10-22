{% import "macros/dotnet" as dotnet %}
using Grpc.Core;
using {{ ProjectName }}.API;
using {{ ProjectName }}.Persistence.Entities;
using {{ ProjectName }}.Persistence.Models;
using {{ ProjectName }}.Persistence.Repositories;

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
