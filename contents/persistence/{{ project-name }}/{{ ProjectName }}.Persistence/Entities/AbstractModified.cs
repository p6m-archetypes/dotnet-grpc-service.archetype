using System.ComponentModel.DataAnnotations.Schema;

namespace {{ ProjectName }}.Persistence.Entities;

public class AbstractModified : AbstractCreated
{
            
    [Column("modified")]
    public DateTime? Updated { get; set; }
}