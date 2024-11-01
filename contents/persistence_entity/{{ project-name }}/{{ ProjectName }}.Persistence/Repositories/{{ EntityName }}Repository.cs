using Microsoft.EntityFrameworkCore;
using {{ ProjectName }}.Persistence.Models;
using {{ ProjectName }}.Persistence.Context;
using {{ ProjectName }}.Persistence.Entities;

namespace {{ ProjectName }}.Persistence.Repositories;
public class {{ EntityName }}Repository
{
    private readonly AppDbContext _context;

    public {{ EntityName }}Repository(AppDbContext context)
    {
        _context = context;
    }

    public void Save({{ EntityName }}Entity {{ entity_name | camel_case }})
    {
        if({{ entity_name | camel_case }} == null)
        {
            throw new ArgumentNullException(nameof({{ entity_name | camel_case }}));
        }

        _context.{{ EntityName | pluralize }}.Add({{ entity_name | camel_case }});
    }

    public IEnumerable<{{ EntityName }}Entity> GetAll()
    {
        return _context.{{ EntityName | pluralize }}.ToList();
    }

    public async Task<IEnumerable<{{ EntityName }}Entity>> GetAllAsync()
    {
        return await _context.{{ EntityName | pluralize }}.ToListAsync();
    }

    public {{ EntityName }}Entity? FindById(string id)
    {
        return _context.{{ EntityName | pluralize }}.Find(id);
    }
    
    public async Task<{{ EntityName }}Entity?> FindByIdAsync(Guid id)
    {
        return await _context.{{ EntityName | pluralize }}.FindAsync(id);
    }

    public void Delete({{ EntityName }}Entity {{ entity_name | camel_case }})
    {
        _context.{{ EntityName | pluralize }}.Remove({{ entity_name | camel_case }});
    }
    

    public async Task<Page<{{ EntityName }}Entity>> FindAsync(PageRequest request)
    {
        var totalRecords = await _context.{{ EntityName | pluralize }}.CountAsync();
        
        var {{ entity_name | camel_case | pluralize }} = await _context.{{ EntityName | pluralize }}
            .OrderBy((i) => i.Name)
            .Skip((request.StartPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new Page<{{ EntityName }}Entity>
        {
            Items = {{ entity_name | camel_case | pluralize }},
            TotalElements = totalRecords
        };
    }

    public async Task<int> SaveAsync({{ EntityName }}Entity {{ entity_name | camel_case }})
    {
        _context.{{ EntityName | pluralize }}.Add({{ entity_name | camel_case }});
        return await _context.SaveChangesAsync();
    }
    
    public async Task<int> UpdateAsync({{ EntityName }}Entity {{ entity_name | camel_case }})
    {
        return await _context.SaveChangesAsync();
    }
}
