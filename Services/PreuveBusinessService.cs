using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;
using Projet.Services.Interfaces;

namespace Projet.Services;

public class PreuveBusinessService(
    MySqlContext context,
    ITypePreuveBusinessService typePreuveBusinessService
) : IPreuveBusinessService
{
    private readonly MySqlContext _context = context;
    private readonly ITypePreuveBusinessService _typePreuveBusinessService = typePreuveBusinessService;

    #region CREATE

    /// <summary>
    /// Créer une preuve
    /// </summary>
    /// <param name="createPreuveRequest">éléments de la preuve</param>
    /// <returns>id de la preuve</returns>
    /// <exception cref="KeyNotFoundException">le dossier ou le type de la preuve n'existe pas</exception>
    public async Task<int> CreatePreuve(CreatePreuveRequest createPreuveRequest)
    {
        var dossierExists = await _context.Dossiers.AnyAsync(d => d.Id == createPreuveRequest.DossierId);
        var typePreuveExists = await _context.TypePreuves.AnyAsync(tp => tp.Id == createPreuveRequest.TypePreuveId);

        if (!dossierExists || !typePreuveExists)
        {
            throw new KeyNotFoundException("DossierId ou TypePreuveId invalide");
        }

        var preuve = createPreuveRequest.ToEntity();
        _context.Preuves.Add(preuve);
        await _context.SaveChangesAsync();
        return preuve.Id;
    }

    #endregion

    #region GET

    // by id

    /// <summary>
    /// Récupère une preuve par son id
    /// </summary>
    /// <param name="id">id de la preuve</param>
    /// <returns>la preuve</returns>
    /// <exception cref="ArgumentException">id invaldie</exception>
    /// <exception cref="KeyNotFoundException">l'id n'existe pas</exception>
    public async Task<PreuveResponse> GetPreuveById(int? id)
    {
        if (id == null)
        {
            throw new ArgumentException("Id requis");
        }

        var preuve = await _context.Preuves.FindAsync(id);

        if (preuve == null)
        {
            throw new KeyNotFoundException("Preuve non trouvée");
        }

        return PreuveResponse.ToPreuveResponse(preuve);
    }

    /// <summary>
    /// Récupère une preuve par son id avec le type de preuve
    /// </summary>
    /// <param name="id">id de la preuve</param>
    /// <returns>la preuve et son type</returns>
    /// <exception cref="ArgumentException">id invalide</exception>
    /// <exception cref="KeyNotFoundException">l'id n'existe pas</exception>
    public async Task<PreuveAndTypeResponse> GetPreuveAndTypeById(int? id)
    {
        if (id == null)
        {
            throw new ArgumentException("Id requis");
        }

        var preuve = await _context.Preuves.FindAsync(id);

        if (preuve == null)
        {
            throw new KeyNotFoundException("Preuve non trouvée");
        }

        return PreuveAndTypeResponse.ToPreuveAndTypeResponse(preuve);
    }

    // by dossierId

    /// <summary>
    /// Récupère toutes les preuves d'un dossier
    /// </summary>
    /// <param name="dossierId">l'id du dossier</param>
    /// <returns>la preuve</returns>
    /// <exception cref="ArgumentException">id invalide</exception>
    /// <exception cref="KeyNotFoundException">l'id n'existe pas</exception>
    public async Task<IList<PreuveResponse>> GetPreuveByDossierId(int? dossierId)
    {
        if (dossierId == null)
        {
            throw new ArgumentException("DossierId requis");
        }

        var dossierExist = await _context.Dossiers.AnyAsync(d => d.Id == dossierId);

        if (!dossierExist)
        {
            throw new KeyNotFoundException("Dossier non trouvé");
        }

        var preuves = await _context.Preuves
            .Where(p => p.DossierId == dossierId)
            .ToListAsync();

        return PreuveResponse.ToListPreuveResponse(preuves);
    }

    /// <summary>
    /// Récupère toutes les preuves d'un dossier avec le type de preuve
    /// </summary>
    /// <param name="dossierId">id du dossier</param>
    /// <returns>les preuves et leur type</returns>
    /// <exception cref="ArgumentException">id invaldie</exception>
    /// <exception cref="KeyNotFoundException">l'id n'existe pas</exception>
    public async Task<IList<PreuveAndTypeResponse>> GetPreuveAndTypeByDossierId(int? dossierId)
    {
        if (dossierId == null)
        {
            throw new ArgumentException("DossierId requis");
        }

        var dossierExist = await _context.Dossiers.AnyAsync(d => d.Id == dossierId);

        if (!dossierExist)
        {
            throw new KeyNotFoundException("Dossier non trouvé");
        }

        var preuves = await _context.Preuves
            .Where(p => p.DossierId == dossierId)
            .Include(p => p.TypePreuveEntity)
            .ToListAsync();

        return PreuveAndTypeResponse.ToListPreuveAndTypeResponse(preuves);
    }

    // by typePreuveId

    /// <summary>
    /// Récupère toutes les preuves d'un type de preuve
    /// </summary>
    /// <param name="typePreuveId">l'id du type de preuve</param>
    /// <returns>liste de preuves</returns>
    /// <exception cref="ArgumentException">id invalide</exception>
    /// <exception cref="KeyNotFoundException">l'id n'existe pas</exception>
    public async Task<IList<PreuveResponse>> GetPreuveByTypePreuveId(int? typePreuveId)
    {
        if (typePreuveId == null)
        {
            throw new ArgumentException("TypePreuveId requis");
        }

        var typePreuveExist = await _context.TypePreuves.AnyAsync(tp => tp.Id == typePreuveId);

        if (!typePreuveExist)
        {
            throw new KeyNotFoundException("TypePreuve non trouvé");
        }

        var preuves = await _context.Preuves
            .Where(p => p.TypePreuveId == typePreuveId)
            .ToListAsync();

        return PreuveResponse.ToListPreuveResponse(preuves);
    }

    // all

    /// <summary>
    /// Récupère toutes les preuves
    /// </summary>
    /// <param name="from">à partir de from</param>
    /// <param name="nb">prend nb résultats</param>
    /// <returns>liste de preuves</returns>
    /// <exception cref="ArgumentException">from et nb doivent être positifs</exception>
    public async Task<IList<PreuveResponse>> GetAllLastPreuves(int from = 0, int nb = 20)
    {
        if (from < 0 || nb < 0)
        {
            throw new ArgumentException("From et nb doivent être positifs");
        }

        var preuves = await _context.Preuves
            .OrderByDescending(p => p.CreatedAt)
            .Skip(from)
            .Take(nb)
            .ToListAsync();

        return PreuveResponse.ToListPreuveResponse(preuves);
    }

    /// <summary>
    /// Récupère toutes les preuves avec leur type
    /// </summary>
    /// <param name="from">partir de from</param>
    /// <param name="nb">prend nb résultats</param>
    /// <returns>liste de preuves et leur type</returns>
    /// <exception cref="ArgumentException">from et/ou nb négatif</exception>
    public async Task<IList<PreuveAndTypeResponse>> GetAllLastPreuvesAndType(int from = 0, int nb = 20)
    {
        if (from < 0 || nb < 0)
        {
            throw new ArgumentException("From et nb doivent être positifs");
        }

        var preuves = await _context.Preuves
            .OrderByDescending(p => p.CreatedAt)
            .Skip(from)
            .Take(nb)
            .Include(p => p.TypePreuveEntity)
            .ToListAsync();

        return PreuveAndTypeResponse.ToListPreuveAndTypeResponse(preuves);
    }

    #endregion

    #region UPDATE

    /// <summary>
    /// Met à jour le contenu d'une preuve
    /// </summary>
    /// <param name="id">id de la preuve</param>
    /// <param name="nouveauContenu">nouveau contenu</param>
    /// <returns>true si la mise a jour a réussi</returns>
    /// <exception cref="ArgumentException">id et/ou nouveauContenu invalide</exception>
    /// <exception cref="KeyNotFoundException">la preuve n'existe pas</exception>
    public async Task<bool> UpdatePreuveContenu(int? id, string? nouveauContenu)
    {
        if (id == null || String.IsNullOrWhiteSpace(nouveauContenu))
        {
            throw new ArgumentException("Id et contenu requis");
        }

        var preuve = await _context.Preuves.FindAsync(id);

        if (preuve == null)
        {
            throw new KeyNotFoundException("Preuve non trouvée");
        }

        preuve.Contenu = nouveauContenu;
        await _context.SaveChangesAsync();
        return true;
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Supprime une preuve par son id
    /// </summary>
    /// <param name="id">id de la preuve</param>
    /// <returns>true si la suppression à réussi</returns>
    /// <exception cref="ArgumentException">id invalide</exception>
    /// <exception cref="KeyNotFoundException">l'id n'existe pas</exception>
    public async Task<bool> DeletePreuveById(int? id)
    {
        if (id == null)
        {
            throw new ArgumentException("Id requis");
        }

        var preuve = _context.Preuves.Find(id);

        if (preuve == null)
        {
            throw new KeyNotFoundException("Preuve non trouvée");
        }

        _context.Preuves.Remove(preuve);
        await _context.SaveChangesAsync();
        return true;
    }

    #endregion
}