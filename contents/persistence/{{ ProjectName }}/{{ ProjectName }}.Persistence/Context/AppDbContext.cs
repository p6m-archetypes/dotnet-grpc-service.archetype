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
    }
}
