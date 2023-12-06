using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;
using Projet.Services.Interfaces;

namespace Projet.Services;

public class UtilisateurService : IUtilisateurService
{
    private readonly MySqlContext _context;

    public UtilisateurService(MySqlContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Créer un utilisateur
    /// </summary>
    /// <param name="utilisateurRequest"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<int> InscriptionUtilisateur(UtilisateurRequest utilisateurRequest)
    {

        if (String.IsNullOrWhiteSpace(utilisateurRequest.Pseudo) || String.IsNullOrWhiteSpace(utilisateurRequest.MotDePasse))
        {
            throw new ArgumentException("Pseudo et mot de passe sont requis");
        }

        if (!await IsPseudoLibre(utilisateurRequest.Pseudo))
        {
            throw new InvalidOperationException("Pseudo déjà utilisé");
        }

        
        var utilisateur = UtilisateurRequest.ToEntity(utilisateurRequest);

        _context.Utilisateurs.Add(utilisateur);
        await _context.SaveChangesAsync();

        return utilisateur.Id;
    }

    /// <summary>
    /// Vérifie si le pseudo est déjà utilisé
    /// </summary>
    /// <param name="pseudo"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<bool> IsPseudoLibre(string pseudo)
    {
        if (String.IsNullOrWhiteSpace(pseudo))
        {
            throw new ArgumentException("Pseudo requis");
        }

        bool isPseudoAlreadyUsed = await _context.Utilisateurs.AnyAsync(u => u.Pseudo == pseudo);
        return !isPseudoAlreadyUsed;
    }
    
    /// <summary>
    /// Récupère un utilisateur par son id
    /// </summary>
    /// <param name="id">l'id de l'utilisateur</param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<UtilisateurResponse> GetUtilisateurById(int id)
    {
        var utilisateur = await _context.Utilisateurs.FindAsync(id);

        if (utilisateur == null)
        {
            throw new KeyNotFoundException("Utilisateur non trouvé");
        }

        return new UtilisateurResponse(utilisateur);
    }

    public async Task<UtilisateurResponse> GetUtilisateurByPseudo(string pseudo)
    {
        var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Pseudo == pseudo);

        if (utilisateur == null)
        {
            throw new InvalidOperationException("Utilisateur non trouvé");
        }

        return new UtilisateurResponse(utilisateur);
    }

    public async Task<UtilisateurEntity> GetFullUtilisateurByPseudo(string pseudo)
    {
        var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Pseudo == pseudo);

        if (utilisateur == null)
        {
            throw new InvalidOperationException("Utilisateur non trouvé");
        }

        return utilisateur;
    }
}
