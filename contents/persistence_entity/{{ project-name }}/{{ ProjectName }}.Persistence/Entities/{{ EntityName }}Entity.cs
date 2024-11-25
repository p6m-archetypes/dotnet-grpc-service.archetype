namespace {{ ProjectName }}.Persistence.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("{{ entity_name }}")]
public class {{ EntityName }}Entity : AbstractEntity<Guid>
{
    [Column("name")]
    [Required]
    public string? Name { get; set; }
}
