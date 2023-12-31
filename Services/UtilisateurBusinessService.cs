using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;
using Projet.Services.Interfaces;

namespace Projet.Services;

public class UtilisateurBusinessService(
    MySqlContext context
) : IUtilisateurBusinessService
{
    private readonly MySqlContext _context = context;

    #region CREATE

    /// <summary>
    /// Créer un utilisateur
    /// </summary>
    /// <param name="utilisateurRequest">information de l'utilisateur</param>
    /// <returns>l'id de l'utilisateur</returns>
    /// <exception cref="ArgumentException">pseudo ou mot de passe invalide</exception>
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

    #endregion

    #region GET

    /// <summary>
    /// Vérifie si le pseudo est déjà utilisé
    /// </summary>
    /// <param name="pseudo">le pseudo à vérifier</param>
    /// <returns>trye si le pseudo est libre, false s'il est déjà utilisé</returns>
    /// <exception cref="ArgumentException">pseudo invalide</exception>
    public async Task<bool> IsPseudoLibre(string? pseudo)
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
    /// <returns>l'utilisateur</returns>
    /// <exception cref="ArgumentException">id invalide</exception>
    /// <exception cref="KeyNotFoundException">l'id n'existe pas</exception>
    public async Task<UtilisateurResponse> GetUtilisateurById(int? id)
    {
        if (id == null)
        {
            throw new ArgumentException("L'id de l'utilisateur est requis");
        }

        var utilisateur = await _context.Utilisateurs.FindAsync(id);

        if (utilisateur == null)
        {
            throw new KeyNotFoundException("Utilisateur non trouvé");
        }

        return new UtilisateurResponse(utilisateur);
    }

    /// <summary>
    /// Récupère un utilisateur par son pseudo
    /// </summary>
    /// <param name="pseudo">le pseudo</param>
    /// <returns>l'utilisateur</returns>
    /// <exception cref="ArgumentException">pseudo invalide</exception>
    /// <exception cref="InvalidOperationException">pseudo n'existe pas</exception>
    public async Task<UtilisateurResponse> GetUtilisateurByPseudo(string? pseudo)
    {
        if (String.IsNullOrWhiteSpace(pseudo))
        {
            throw new ArgumentException("Le pseudo est requis");
        }

        var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Pseudo == pseudo);

        if (utilisateur == null)
        {
            throw new InvalidOperationException("Utilisateur non trouvé");
        }

        return new UtilisateurResponse(utilisateur);
    }

    /// <summary>
    /// Récupère un utilisateur par son pseudo avec son mot de passe
    /// </summary>
    /// <param name="pseudo">le pseudo</param>
    /// <returns>l'utilisateur</returns>
    /// <exception cref="ArgumentException">pseudo invalide</exception>
    /// <exception cref="InvalidOperationException">le pseudo n'existe pas</exception>
    public async Task<UtilisateurEntity> GetFullUtilisateurByPseudo(string? pseudo)
    {
        if (String.IsNullOrWhiteSpace(pseudo))
        {
            throw new ArgumentException("Le pseudo est requis");
        }

        var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Pseudo == pseudo);

        if (utilisateur == null)
        {
            throw new InvalidOperationException("Utilisateur non trouvé");
        }

        return utilisateur;
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Change le pseudo d'un utilisateur
    /// </summary>
    /// <param name="changeUtilisateurPseudoRequest">informations pour le changement de pseudo</param>
    /// <returns>true si le changement a réussi</returns>
    /// <exception cref="KeyNotFoundException">l'utilisateur n'existe pas</exception>
    public async Task<bool> UpdateUtilisateurPseudoById(ChangeUtilisateurPseudoRequest changeUtilisateurPseudoRequest)
    {
        var utilisateur = _context.Utilisateurs.Find(changeUtilisateurPseudoRequest.UtilisateurId);

        if (utilisateur == null)
        {
            throw new KeyNotFoundException("L'utilisateur n'existe pas");
        }

        utilisateur.Pseudo = changeUtilisateurPseudoRequest.NewPseudo;
        _context.Utilisateurs.Update(utilisateur);
        await _context.SaveChangesAsync();
        
        return true;
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Supprime un utilisateur par son id
    /// </summary>
    /// <param name="id">id de l'utilisateur</param>
    /// <returns>true si la suppression à réussi</returns>
    /// <exception cref="ArgumentException">id invalide</exception>
    /// <exception cref="KeyNotFoundException">l'id n'existe pas</exception>
    public async Task<bool> DeleteUtilisateurById(int? id)
    {
        if (id == null)
        {
            throw new ArgumentException("L'id de l'utilisateur est requis");
        }

        var utilisateur = _context.Utilisateurs.Find(id);

        if (utilisateur == null)
        {
            throw new KeyNotFoundException("L'utilisateur n'existe pas");
        }

        _context.Utilisateurs.Remove(utilisateur);
        await _context.SaveChangesAsync();
        
        return true;
    }

    #endregion
}
