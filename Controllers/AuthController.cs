using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;
using Projet.Models.Entity;
using Projet.Services;
using Projet.Services.Interfaces;

namespace Projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService
        )
        {
            _authService = authService;
        }

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

        [HttpGet("isAdmin/{id}")]
        public async Task<ActionResult<bool>> IsAdmin([FromRoute] int? id)
        {
            bool isAdmin;
            try
            {
                isAdmin = await _authService.IsAdmin(id);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(isAdmin);
        }
    }
}
