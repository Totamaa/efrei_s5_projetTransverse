using System.ComponentModel.DataAnnotations;

namespace Projet.Models.Entity;

public class TypePreuveEntity : BaseEntity
{
    [Required]
    public string Nom { get; set; }

    public virtual IList<PreuveEntity>? Preuves { get; set; }

}
