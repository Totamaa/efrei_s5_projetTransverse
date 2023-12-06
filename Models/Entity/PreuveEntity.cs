using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet.Models.Entity
{
    public class PreuveEntity : BaseEntity
    {
        [ForeignKey("DossierEntity")]
        [Required]
        public int DossierId { get; set; }

        [ForeignKey("TypePreuveEntity")]
        [Required]
        public int TypePreuveId { get; set; }

        public string? Contenu { get; set; }

        public virtual DossierEntity? DossierEntity { get; set; }

        public virtual TypePreuveEntity? TypePreuveEntity { get; set; }
    }
}
