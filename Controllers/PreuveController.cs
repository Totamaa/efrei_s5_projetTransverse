using Microsoft.AspNetCore.Mvc;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Services.Interfaces;

namespace Projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreuveController(
        IPreuveBusinessService preuveBusinessService
    ) : ControllerBase
    {
        private readonly IPreuveBusinessService _preuveBusinessService = preuveBusinessService;

        #region POST

        [HttpPost("create")]
        public async Task<ActionResult<int>> CreatePreuve([FromBody] CreatePreuveRequest createPreuveRequest)
        {
            int preuveId;
            try
            {
                preuveId = await _preuveBusinessService.CreatePreuve(createPreuveRequest);
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
            return CreatedAtAction(nameof(GetPreuveById), new {id = preuveId}, null);
        }

        #endregion

        #region GET

        // by id

        [HttpGet("{id}")]
        public async Task<ActionResult<PreuveResponse>> GetPreuveById([FromRoute] int id)
        {
            PreuveResponse preuve;
            try
            {
                preuve = await _preuveBusinessService.GetPreuveById(id);
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
            return Ok(preuve);
        }

        [HttpGet("andType/{id}")]
        public async Task<ActionResult<PreuveAndTypeResponse>> GetPreuveAndTypeById([FromRoute] int id)
        {
            PreuveAndTypeResponse preuveAndType;
            try
            {
                preuveAndType = await _preuveBusinessService.GetPreuveAndTypeById(id);
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
            return Ok(preuveAndType);
        }

        // by dossier id

        [HttpGet("dossier/{dossierId}")]
        public async Task<ActionResult<IList<PreuveResponse>>> GetPreuveByDossierId([FromRoute] int dossierId)
        {
            IList<PreuveResponse> preuves;
            try
            {
                preuves = await _preuveBusinessService.GetPreuveByDossierId(dossierId);
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
            return Ok(preuves);
        }

        [HttpGet("andType/dossier/{dossierId}")]
        public async Task<ActionResult<IList<PreuveAndTypeResponse>>> GetPreuveAndTypeByDossierId([FromRoute] int dossierId)
        {
            IList<PreuveAndTypeResponse> preuvesAndType;
            try
            {
                preuvesAndType = await _preuveBusinessService.GetPreuveAndTypeByDossierId(dossierId);
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

            return Ok(preuvesAndType);
        }

        // by type preuve id

        [HttpGet("typePreuve/{typePreuveId}")]
        public async Task<ActionResult<IList<PreuveResponse>>> GetPreuveByTypePreuveId([FromRoute] int typePreuveId)
        {
            IList<PreuveResponse> preuves;
            try
            {
                preuves = await _preuveBusinessService.GetPreuveByTypePreuveId(typePreuveId);
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
            return Ok(preuves);
        }

        // get all

        [HttpGet("all")]
        public async Task<ActionResult<IList<PreuveResponse>>> GetAll([FromQuery]int from = 0,[FromQuery] int nb = 20)
        {
            IList<PreuveResponse> preuves;
            try
            {
                preuves = await _preuveBusinessService.GetAllLastPreuves(from, nb);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(preuves);
        }

        [HttpGet("andType/all")]
        public async Task<ActionResult<IList<PreuveAndTypeResponse>>> GetAllAndType([FromQuery]int from = 0,[FromQuery] int nb = 20)
        {
            IList<PreuveAndTypeResponse> preuvesAndType;
            try
            {
                preuvesAndType = await _preuveBusinessService.GetAllLastPreuvesAndType(from, nb);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(preuvesAndType);
        }

        #endregion

        #region PATCH

        [HttpPatch("updateContenu/{id}")]
        public async Task<ActionResult<bool>> UpdatePreuveContenu([FromRoute] int id, [FromBody] string nouveauContenu)
        {
            bool preuveUpdated;
            try
            {
                preuveUpdated = await _preuveBusinessService.UpdatePreuveContenu(id, nouveauContenu);
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

            return Ok(preuveUpdated);
        }

        #endregion
        
        #region DELETE

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            bool preuveDeleted;
            try
            {
                preuveDeleted = await _preuveBusinessService.DeletePreuveById(id);
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