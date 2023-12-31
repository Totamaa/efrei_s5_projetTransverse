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
    public class TypePreuveController(
        ITypePreuveBusinessService typePreuveBusinessService
    ) : ControllerBase
    {
        private readonly ITypePreuveBusinessService _typePreuveBusinessService = typePreuveBusinessService;

        #region GET

        [HttpGet("all")]
        public async Task<ActionResult<IList<TypePreuveResponse>>> GetAll()
        {
            IList<TypePreuveResponse> typePreuves;
            try
            {
                typePreuves = await _typePreuveBusinessService.GetAll();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(typePreuves);
        }
        
        #endregion
    }
}