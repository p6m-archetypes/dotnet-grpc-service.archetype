namespace {{ ProjectName }}.Persistence.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("{{ entity_name }}")]
public class {{ EntityName }}Entity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [Required]
    public string? Name { get; set; }
}
