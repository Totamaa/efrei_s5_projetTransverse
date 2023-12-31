using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;

namespace Projet.Services.Interfaces;

public interface IUtilisateurBusinessService
{
    #region CREATE
    Task<int> InscriptionUtilisateur(UtilisateurRequest utilisateurRequest);

    #endregion

    #region GET
    Task<bool> IsPseudoLibre(string? pseudo);
    Task<UtilisateurResponse> GetUtilisateurById(int? id);
    Task<UtilisateurResponse> GetUtilisateurByPseudo(string? pseudo);
    Task<UtilisateurEntity> GetFullUtilisateurByPseudo(string? pseudo);

    #endregion

    #region UPDATE
    Task<bool> UpdateUtilisateurPseudoById(ChangeUtilisateurPseudoRequest changeUtilisateurPseudoRequest);

    #endregion

    #region DELETE
    Task<bool> DeleteUtilisateurById(int? id);

    #endregion
}
