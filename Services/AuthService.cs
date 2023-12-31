using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;
using Projet.Services.Interfaces;

namespace Projet.Services;

public class AuthService(
    MySqlContext context,
    IUtilisateurBusinessService utilisateurBusinessService
) : IAuthService
{
    private readonly MySqlContext _context = context;
    private readonly IUtilisateurBusinessService _utilisateurBusinessService = utilisateurBusinessService;

    #region GET

    /// <summary>
    /// Connexion d'un utilisateur
    /// </summary>
    /// <param name="utilisateurRequest">La request avec le pseudo et le mot de passe</param>
    /// <returns>true si c'est le bon mot de passe</returns>
    /// <exception cref="ArgumentException">Si il manque un paramètre</exception>
    public async Task<bool> ConnexionUtilisateur(UtilisateurRequest utilisateurRequest)
    {
        if (String.IsNullOrWhiteSpace(utilisateurRequest.Pseudo) || String.IsNullOrWhiteSpace(utilisateurRequest.MotDePasse))
        {
            throw new ArgumentException("Pseudo et mot de passe sont requis");
        }

        UtilisateurEntity utilisateur = await _utilisateurBusinessService.GetFullUtilisateurByPseudo(utilisateurRequest.Pseudo);

        if (utilisateur.MotDePasse == utilisateurRequest.MotDePasse)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// Vérifie si l'utilisateur est admin
    /// </summary>
    /// <param name="idUtilisateur">l'id de l'utilisateur à vérifier</param>
    /// <returns>true si l'utilisateur est admin, false sinon</returns>
    /// <exception cref="KeyNotFoundException">l'id n'existe pas</exception>
    public Task<bool> IsAdmin(int? idUtilisateur)
    {
        if (idUtilisateur == null)
        {
            throw new KeyNotFoundException("Utilisateur non trouvé");
        }

        return _context.Utilisateurs.AnyAsync(u => u.Id == idUtilisateur && u.IsAdmin);
    }

    #endregion
}
