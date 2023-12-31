using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;

namespace Projet.Services.Interfaces;

public interface IDossierBusinessService
{
    #region CREATE
    Task<int> CreateDossier(int? utilisateurId);

    #endregion

    #region GET
    Task<DossierResponse> GetDossierById(int? id);
    Task<IList<DossierResponse>> GetDossierByUtilisateurId(int? utilisateurId);
    Task<IList<DossierResponse>> GetAllLastDossiers(int from = 0, int nb = 20);

    #endregion

    #region DELETE
    Task<bool> DeleteDossierById(int? id);

    #endregion
}