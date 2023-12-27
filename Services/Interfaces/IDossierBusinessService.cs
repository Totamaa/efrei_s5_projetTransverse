using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;

namespace Projet.Services.Interfaces;

public interface IDossierBusinessService
{
    Task<int> CreateDossier(int? utilisateurId);
    Task<DossierResponse> GetDossierById(int? id);
    Task<IList<DossierResponse>> GetDossierByUtilisateurId(int? utilisateurId);
    Task<bool> DeleteDossierById(int? id);
    Task<IList<DossierResponse>> GetAllLastDossiers(int from = 0, int nb = 20);
}