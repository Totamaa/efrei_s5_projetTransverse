using Microsoft.AspNetCore.Mvc;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Services.Interfaces;

namespace Projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;

        public UtilisateurController(
            IUtilisateurService utilisateurService
        )
        {
            _utilisateurService = utilisateurService;
        }

        [HttpPost("inscription")]
        public async Task<ActionResult<int>> InscriptionUtilisateur([FromQuery] UtilisateurRequest utilisateurRequest)
        {
            int utilisateurId;
            try{
                utilisateurId = await _utilisateurService.InscriptionUtilisateur(utilisateurRequest);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return CreatedAtAction(nameof(GetUtilisateurById), new {id = utilisateurId}, utilisateurId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurResponse>> GetUtilisateurById([FromRoute] int id)
        {
            UtilisateurResponse utilisateur;
            try
            {
                utilisateur = await _utilisateurService.GetUtilisateurById(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(utilisateur);
        }
    }
}
