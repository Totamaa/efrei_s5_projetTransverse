using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet.Models.Entity;

public class DossierEntity(
    int utilisateurId,
    int cyberharceleurId
) : BaseEntity
{
    [ForeignKey("UtilisateurEntity")]
    [Required]
    public int UtilisateurId { get; set; } = utilisateurId;

    [ForeignKey("CyberharceleurEntity")]
    [Required]
    public int CyberharceleurId { get; set; } = cyberharceleurId;

    public virtual UtilisateurEntity? UtilisateurEntity { get; set; }
    public virtual CyberharceleurEntity? CyberharceleurEntity { get; set; }
    public virtual IList<PreuveEntity>? Preuves { get; set; }
    
}
