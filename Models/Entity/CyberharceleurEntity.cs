namespace Projet.Models.Entity
{
    public class CyberharceleurEntity : BaseEntity
    {
        public string Pseudo { get; set; }

        public string Description { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        // Relation avec Dossier
        public virtual IList<DossierEntity>? Dossiers { get; set; }
    }
}