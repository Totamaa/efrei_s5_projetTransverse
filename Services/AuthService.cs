using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;
using Projet.Services.Interfaces;

namespace Projet.Services;

public class AuthService : IAuthService
{
    private readonly MySqlContext _context;
    private readonly IUtilisateurService _utilisateurService;

    public AuthService(
        MySqlContext context,
        IUtilisateurService utilisateurService
    )
    {
        _context = context;
        _utilisateurService = utilisateurService;
    }

    /// <summary>
    /// Connexion d'un utilisateur
    /// </summary>
    /// <param name="utilisateurRequest"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<bool> ConnexionUtilisateur(UtilisateurRequest utilisateurRequest)
    {
        if (String.IsNullOrWhiteSpace(utilisateurRequest.Pseudo) || String.IsNullOrWhiteSpace(utilisateurRequest.MotDePasse))
        {
            throw new ArgumentException("Pseudo et mot de passe sont requis");
        }

        UtilisateurEntity utilisateur = await _utilisateurService.GetFullUtilisateurByPseudo(utilisateurRequest.Pseudo);

        if (utilisateur.MotDePasse == utilisateurRequest.MotDePasse)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
