using System.ComponentModel.DataAnnotations;

namespace Projet.Models.Entity;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
