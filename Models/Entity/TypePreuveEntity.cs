using System.ComponentModel.DataAnnotations;

namespace Projet.Models.Entity;

public class TypePreuveEntity(
    string nom
) : BaseEntity
{
    [Required]
    public string Nom { get; set; } = nom;

    public virtual IList<PreuveEntity>? Preuves { get; set; }

}
