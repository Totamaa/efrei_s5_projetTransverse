using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet.Models.Entity;

public class DossierEntity : BaseEntity
{
    [ForeignKey("UtilisateurEntity")]
    [Required]
    public Guid UtilisateurId { get; set; }

    [ForeignKey("CyberharceleurEntity")]
    [Required]
    public Guid CyberharceleurId { get; set; }

    public virtual UtilisateurEntity? UtilisateurEntity { get; set; }
    public virtual CyberharceleurEntity? CyberharceleurEntity { get; set; }
    public virtual IList<Preuve>? Preuves { get; set; }
    
}
