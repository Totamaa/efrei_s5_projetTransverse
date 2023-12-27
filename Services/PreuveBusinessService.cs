using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;
using Projet.Services.Interfaces;

namespace Projet.Services;

public class PreuveBusinessService : IPreuveBusinessService
{
    private readonly MySqlContext _context;
    private readonly ITypePreuveBusinessService _typePreuveBusinessService;

    public PreuveBusinessService(
        MySqlContext context,
        ITypePreuveBusinessService typePreuveBusinessService
    )
    {
        _context = context;
        _typePreuveBusinessService = typePreuveBusinessService;
    }

    public async Task<int> CreatePreuve(CreatePreuveRequest createPreuveRequest)
    {
        if (createPreuveRequest.DossierId == null || createPreuveRequest.TypePreuveId == null || createPreuveRequest.Contenu == null)
        {
            throw new ArgumentException("DossierId, TypePreuveId et Contenu sont requis");
        }

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

    public async Task<bool> UpdatePreuveContenu(int? id, string? nouveauContenu)
    {
        if (id == null)
        {
            throw new ArgumentException("Id requis");
        }

        if (String.IsNullOrWhiteSpace(nouveauContenu))
        {
            throw new ArgumentException("Contenu requis");
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
}