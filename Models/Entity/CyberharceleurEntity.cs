using System.ComponentModel.DataAnnotations;

namespace Projet.Models.Entity
{
    public class CyberharceleurEntity(
        string pseudo,
        string? description = null,
        string? lastName = null,
        string? firstName = null 
    ) : BaseEntity
    {
        [Required]
        public string Pseudo { get; set; } = pseudo;

        public string? Description { get; set; } = description;

        public string? LastName { get; set; } = lastName;

        public string? FirstName { get; set; } = firstName;

        // Relation avec Dossier
        public virtual IList<DossierEntity>? Dossiers { get; set; }
    }
}