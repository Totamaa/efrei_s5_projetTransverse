using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;

namespace Projet.Services.Interfaces;

public interface IAuthService
{
    Task<bool> ConnexionUtilisateur(UtilisateurRequest utilisateurRequest);
}