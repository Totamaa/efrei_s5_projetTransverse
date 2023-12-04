using System.ComponentModel.DataAnnotations;

namespace Projet.Models.Entity;

public class UtilisateurEntity : BaseEntity
{
    [Required]
    public string? Pseudo { get; set; }

    [Required]
    public string? MotDePasse { get; set; }

    public bool IsAdmin { get; set; }

    public virtual IList<DossierEntity>? Dossiers { get; set; }
}
