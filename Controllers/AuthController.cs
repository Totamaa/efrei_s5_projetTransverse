using Microsoft.AspNetCore.Mvc;
using Projet.Models.DTO.Request;
using Projet.Services.Interfaces;

namespace Projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        IAuthService authService
    ) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        #region POST

        [HttpPost("connexion")]
        public async Task<IActionResult> ConnexionUtilisateur([FromBody] UtilisateurRequest utilisateurRequest)
        {
            try
            {
                bool isUtilisateurConnected = await _authService.ConnexionUtilisateur(utilisateurRequest);

                if (isUtilisateurConnected)
                {
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        #endregion

        #region GET

        [HttpGet("isAdmin/{id}")]
        public async Task<ActionResult<bool>> IsAdmin([FromRoute] int id)
        {
            bool isAdmin;
            try
            {
                isAdmin = await _authService.IsAdmin(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(isAdmin);
        }

        #endregion
    }
}
