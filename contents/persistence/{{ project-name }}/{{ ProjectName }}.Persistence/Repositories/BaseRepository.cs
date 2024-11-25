using {{ ProjectName }}.Persistence.Context;
using {{ ProjectName }}.Persistence.Entities;
using {{ ProjectName }}.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace {{ ProjectName }}.Persistence.Repositories;

public  abstract class BaseRepository<TEntity, TEntityId>
    where TEntity: AbstractEntity<TEntityId>
{
    protected readonly AppDbContext DbContext;

    protected BaseRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public void Save(TEntity entity)
    {
        if(entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        DbContext.Set<TEntity>().Add(entity);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return DbContext.Set<TEntity>().ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbContext.Set<TEntity>().ToListAsync();
    }

    public TEntity? FindById(string id)
    {
        return DbContext.Set<TEntity>().Find(id);
    }
    
    public async Task<TEntity?> FindByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }

    public void Delete(TEntity example)
    {
        DbContext.Set<TEntity>().Remove(example);
    }
    
    public async Task<Page<TEntity>> FindAsync(PageRequest request)
    {
        var totalRecords = await DbContext.Set<TEntity>().CountAsync();
        
        var entities = await DbContext.Set<TEntity>()
            .OrderBy((i) => i.Created)
            .Skip((request.StartPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new Page<TEntity>
        {
            Items = entities,
            TotalElements = totalRecords
        };
    }
    
    public void Update(TEntity entity)
    {
        DbContext.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Save()
    {
        DbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}