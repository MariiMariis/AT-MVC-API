using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Processadores.WebApi.Controllers
{
    
    [ApiController] 
    [Route("api/v1/[controller]")]
    public class FabricanteApiController : ControllerBase
    {
        private readonly IFabricanteService _fabricanteService;
       

        public FabricanteApiController(
            IFabricanteService fabricanteService)
        {
            _fabricanteService = fabricanteService;
           
        }

        [HttpGet("{orderAscendant:bool}/{search?}")]
        public async Task<ActionResult<IEnumerable<FabricanteModel>>> Get
            (bool orderAscendant,
             string search = null)
        {
            var fabricantes = await this._fabricanteService.GetAllAsync(orderAscendant, search);

            return Ok(fabricantes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FabricanteModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var fabricanteModel = await _fabricanteService.GetByIdAsync(id);

            if (fabricanteModel == null)
            {
                return NotFound();
            }

            return Ok(fabricanteModel);
        }

        [HttpPost]
        public async Task<ActionResult<FabricanteModel>> Post([FromBody] FabricanteModel fabricanteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(fabricanteModel);
            }

            var fabricanteCreated = await _fabricanteService.CreateAsync(fabricanteModel);
            

            return Ok(fabricanteCreated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<FabricanteModel>> Put(int id, [FromBody] FabricanteModel fabricanteModel)
        {
            if (id != fabricanteModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(fabricanteModel);
            }

            try
            {
                var userEdited = await _fabricanteService.EditAsync(fabricanteModel);
                
                return Ok(userEdited);
            }
            catch (DbUpdateConcurrencyException)
            {

                return StatusCode(409);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _fabricanteService.DeleteAsync(id);
            
            return Ok();
        }

        [HttpGet("IsNameValid/{nomeFabricante}/{id}")]
        public async Task<IActionResult> IsNameValid(string nomeFabricante, int id)
        {
            var isValid = await _fabricanteService.IsNameValidAsync(nomeFabricante, id);

            return Ok(isValid);
        }
    }
}
