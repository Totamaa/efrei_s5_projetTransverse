using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Projet.Models.Context;
using Projet.Models.DTO.Response;

namespace Projet.Services.Interfaces
{
    public class DossierBusinessService : IDossierBusinessService
    {
        private readonly MySqlContext _context;
        private readonly IUtilisateurBusinessService _utilisateurBusinessService;

        public DossierBusinessService(MySqlContext context, IUtilisateurBusinessService utilisateurBusinessService)
        {
            _context = context;
            _utilisateurBusinessService = utilisateurBusinessService;
        }

        public Task<int> CreateDossier(int? utilisateurId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteDossierById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("L'id du dossier est requis");
            }

            var dossier = _context.Dossiers.Find(id);

            if (dossier == null)
            {
                throw new KeyNotFoundException("Le dossier n'existe pas");
            }

            _context.Dossiers.Remove(dossier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<DossierResponse>> GetAllLastDossiers(int from=0, int nb=20)
        {
            if (from < 0 || nb < 0)
            {
                throw new ArgumentException("Les paramètres from et to doivent être positifs");
            }
            var dossiers = await _context.Dossiers
                .OrderByDescending(d => d.CreatedAt)
                .Skip(from)
                .Take(nb)
                .ToListAsync();
            return DossierResponse.ToListDossierResponse(dossiers);
        }

        public async Task<DossierResponse> GetDossierById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("L'id du dossier est requis");
            }

            var dossier = await _context.Dossiers.FindAsync(id);

            if (dossier == null)
            {
                throw new KeyNotFoundException("Le dossier n'existe pas");
            }

            return DossierResponse.ToDossierResponse(dossier);
        }

        public async Task<IList<DossierResponse>> GetDossierByUtilisateurId(int? utilisateurId)
        {
            if (utilisateurId == null)
            {
                throw new ArgumentException("L'id de l'utilisateur est requis");
            }

            var utilisateur = _utilisateurBusinessService.GetUtilisateurById(utilisateurId);

            if (utilisateur == null)
            {
                throw new KeyNotFoundException("L'utilisateur n'existe pas");
            }

            var dossiers = await _context.Dossiers.Where(d => d.UtilisateurId == utilisateurId).ToListAsync();

            return DossierResponse.ToListDossierResponse(dossiers);
        }
    }
}