using Microsoft.AspNetCore.Mvc;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Services.Interfaces;

namespace Projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController(
        IUtilisateurBusinessService utilisateurBusinessService
    ) : ControllerBase
    {
        private readonly IUtilisateurBusinessService _utilisateurBusinessService = utilisateurBusinessService;

        #region POST

        [HttpPost("inscription")]
        public async Task<ActionResult<int>> InscriptionUtilisateur([FromBody] UtilisateurRequest utilisateurRequest)
        {
            int utilisateurId;
            try{
                utilisateurId = await _utilisateurBusinessService.InscriptionUtilisateur(utilisateurRequest);
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

            return CreatedAtAction(nameof(GetUtilisateurById), new {id = utilisateurId}, null);
        }

        #endregion
        
        #region GET

        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurResponse>> GetUtilisateurById([FromRoute] int id)
        {
            UtilisateurResponse utilisateur;
            try
            {
                utilisateur = await _utilisateurBusinessService.GetUtilisateurById(id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
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

        #endregion

        #region PATCH

        [HttpPatch("changePseudo")]
        public async Task<ActionResult> UpdateUtilisateurPseudoById([FromBody] ChangeUtilisateurPseudoRequest changeUtilisateurPseudoRequest)
        {
            try
            {
                await _utilisateurBusinessService.UpdateUtilisateurPseudoById(changeUtilisateurPseudoRequest);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return NoContent();
        }

        #endregion

        #region DELETE	

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUtilisateurById([FromRoute] int id)
        {
            try
            {
                await _utilisateurBusinessService.DeleteUtilisateurById(id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return NoContent();
        }
    

        #endregion
    }
}
