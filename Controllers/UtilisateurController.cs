using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.Entity;

namespace Projet.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly MySqlContext _context;

        public UtilisateurController(MySqlContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateur
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilisateurEntity>>> GetUtilisateurs()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        // GET: api/Utilisateur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurEntity>> GetUtilisateurEntity(int id)
        {
            var utilisateurEntity = await _context.Utilisateurs.FindAsync(id);

            if (utilisateurEntity == null)
            {
                return NotFound();
            }

            return utilisateurEntity;
        }

        // PUT: api/Utilisateur/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateurEntity(int id, UtilisateurEntity utilisateurEntity)
        {
            if (id != utilisateurEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(utilisateurEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Utilisateur
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UtilisateurEntity>> PostUtilisateurEntity(UtilisateurEntity utilisateurEntity)
        {
            _context.Utilisateurs.Add(utilisateurEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateurEntity", new { id = utilisateurEntity.Id }, utilisateurEntity);
        }

        // DELETE: api/Utilisateur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateurEntity(int id)
        {
            var utilisateurEntity = await _context.Utilisateurs.FindAsync(id);
            if (utilisateurEntity == null)
            {
                return NotFound();
            }

            _context.Utilisateurs.Remove(utilisateurEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilisateurEntityExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.Id == id);
        }
    }
}
