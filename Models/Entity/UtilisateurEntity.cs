using System.ComponentModel.DataAnnotations;

namespace Projet.Models.Entity;

public class UtilisateurEntity(
    string pseudo,
    string motDePasse,
    bool isAdmin = false
) : BaseEntity
{
    [Required]
    public string Pseudo { get; set; } = pseudo;

    [Required]
    public string MotDePasse { get; set; } = motDePasse;

    [Required]
    public bool IsAdmin { get; set; } = isAdmin;

    public virtual IList<DossierEntity>? Dossiers { get; set; }
}
