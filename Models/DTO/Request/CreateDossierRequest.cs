namespace Projet.Models.DTO.Request
{
    public class CreateDossierRequest(
        int utilisateurId
    )
    {
        public int UtilisateurId { get; set; } = utilisateurId;
    }
}