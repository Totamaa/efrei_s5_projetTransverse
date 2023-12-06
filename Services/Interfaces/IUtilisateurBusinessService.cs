using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;

namespace Projet.Services.Interfaces;

public interface IUtilisateurService
{
    Task<int> InscriptionUtilisateur(UtilisateurRequest utilisateurRequest);
    Task<bool> IsPseudoLibre(string pseudo);
    Task<UtilisateurResponse> GetUtilisateurById(int id);
    Task<UtilisateurResponse> GetUtilisateurByPseudo(string pseudo);
    Task<UtilisateurEntity> GetFullUtilisateurByPseudo(string pseudo);

}