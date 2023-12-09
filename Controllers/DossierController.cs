using Microsoft.AspNetCore.Mvc;
using Projet.Models.DTO.Response;
using Projet.Services.Interfaces;

namespace Projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DossierController: ControllerBase
    {
        private readonly DossierBusinessService _DossierBusinessService;

        public DossierController(
            DossierBusinessService DossierBusinessService
        )
        {
            _DossierBusinessService = DossierBusinessService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DossierResponse>> GetDossierById([FromRoute] int? id)
        {
            DossierResponse dossier;
            try
            {
                dossier = await _DossierBusinessService.GetDossierById(id);
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
            return Ok(dossier);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDossierById([FromRoute] int? id)
        {
            try
            {
                await _DossierBusinessService.DeleteDossierById(id);
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

        [HttpGet("all")]
        public async Task<ActionResult<IList<DossierResponse>>> GetAllLastDossiers([FromQuery] int from = 0, [FromQuery] int to = 20)
        {
            IList<DossierResponse> dossiers;
            try
            {
                dossiers = await _DossierBusinessService.GetAllLastDossiers(from, to);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(dossiers);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateDossier([FromQuery] int? utilisateurId)
        {
            int dossierId;
            try
            {
                dossierId = await _DossierBusinessService.CreateDossier(utilisateurId);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (NotImplementedException)
            {
                return StatusCode(501, "Not Implemented");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return CreatedAtAction(nameof(GetDossierById), new {id = dossierId}, dossierId);
        }
    }
}