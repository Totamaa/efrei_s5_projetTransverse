using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;

namespace Projet.Services.Interfaces;

public interface IAuthService
{
    #region GET
    Task<bool> ConnexionUtilisateur(UtilisateurRequest utilisateurRequest);
    Task<bool> IsAdmin(int? idUtilisateur);
    
    #endregion
}
