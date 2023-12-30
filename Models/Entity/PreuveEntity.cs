using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet.Models.Entity
{
    public class PreuveEntity(
        int dossierId,
        int typePreuveId,
        string? contenu = null
    ) : BaseEntity
    {
        [ForeignKey("DossierEntity")]
        [Required]
        public int DossierId { get; set; } = dossierId;

        [ForeignKey("TypePreuveEntity")]
        [Required]
        public int TypePreuveId { get; set; } = typePreuveId;

        public string? Contenu { get; set; } = contenu;

        public virtual DossierEntity? DossierEntity { get; set; }

        public virtual TypePreuveEntity? TypePreuveEntity { get; set; }
    }
}
