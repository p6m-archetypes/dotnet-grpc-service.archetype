using Microsoft.EntityFrameworkCore;
using {{ ProjectName }}.Persistence.Entities;

namespace {{ ProjectName }}.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

{%- for entity_key in model.entities %}
        public DbSet<{{ entity_key | pascal_case }}Entity> {{ entity_key | pascal_case | pluralize }} { get; set; }
{%- endfor %}
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AbstractModified && 
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AbstractModified)entry.Entity).Created = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    ((AbstractModified)entry.Entity).Updated = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AbstractModified && 
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AbstractModified)entry.Entity).Created = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    ((AbstractModified)entry.Entity).Updated = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
