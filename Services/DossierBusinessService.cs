using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Projet.Models.Context;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;

namespace Projet.Services.Interfaces
{
    public class DossierBusinessService(
        MySqlContext context,
        IUtilisateurBusinessService utilisateurBusinessService
    ) : IDossierBusinessService
    {
        private readonly MySqlContext _context = context;
        private readonly IUtilisateurBusinessService _utilisateurBusinessService = utilisateurBusinessService;

        #region CREATE

        public Task<int> CreateDossier(int? utilisateurId)
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

            throw new NotImplementedException();
        }

        #endregion

        #region GET
        
        /// <summary>
        /// Récupère un dossier par son id
        /// </summary>
        /// <param name="id">l'id du dossier</param>
        /// <returns>le dossiers</returns>
        /// <exception cref="ArgumentException">id requis</exception>
        /// <exception cref="KeyNotFoundException">le dossier n'existe pas</exception>
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

        /// <summary>
        /// Récupère tous les dossiers d'un utilisateur
        /// </summary>
        /// <param name="utilisateurId">id de l'utilisateur</param>
        /// <returns>lsite de dossiers</returns>
        /// <exception cref="ArgumentException">id requis</exception>
        /// <exception cref="KeyNotFoundException">l'utilisateur n'existe pas</exception>
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
        
        /// <summary>
        /// Récupère tous les dossiers
        /// </summary>
        /// <param name="from">commence au 'from'ème dossier</param>
        /// <param name="nb">prend nb dossiers</param>
        /// <returns>liste de dossiers</returns>
        /// <exception cref="ArgumentException">from et/ou nb négatif</exception>
        public async Task<IList<DossierResponse>> GetAllLastDossiers(int from=0, int nb=20)
        {
            if (from < 0 || nb < 0)
            {
                throw new ArgumentException("Les paramètres from et nb doivent être positifs");
            }
            var dossiers = await _context.Dossiers
                .OrderByDescending(d => d.CreatedAt)
                .Skip(from)
                .Take(nb)
                .ToListAsync();
            return DossierResponse.ToListDossierResponse(dossiers);
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Supprime un dossier par son id
        /// </summary>
        /// <param name="id">id du dossier à supprimer</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">id invalide</exception>
        /// <exception cref="KeyNotFoundException">le dossier n'existe pas</exception>
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

        #endregion
    }
}