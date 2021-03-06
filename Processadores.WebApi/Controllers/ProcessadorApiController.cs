using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Model.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Processadores.WebApi.Controllers
{
    

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProcessadorApiController : ControllerBase
    {
        private readonly IProcessadorService _processadorService;
        

        public ProcessadorApiController(
            IProcessadorService processadorService)
            
        {
            _processadorService = processadorService;
            
        }

        [HttpGet("{orderAscendant:bool}/{search?}")]
        public async Task<ActionResult<IEnumerable<ProcessadorModel>>> Get(
            bool orderAscendant,
            string search = null)
        {
            var processadores = await this._processadorService
                                .GetAllAsync(orderAscendant, search);

            return Ok(processadores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProcessadorModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var processadorModel = await _processadorService.GetByIdAsync(id);

            if (processadorModel == null)
            {
                return NotFound();
            }

            return Ok(processadorModel);
        }

        [HttpPost]
        public async Task<ActionResult<ProcessadorModel>> Post([FromBody] ProcessadorModel processadorModel)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(processadorModel);
            }
            
            var processadorCreated = await _processadorService.CreateAsync(processadorModel);
            

            return Ok(processadorCreated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProcessadorModel>> Put(int id, [FromBody] ProcessadorModel processadorModel)
        {
            if (id != processadorModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(processadorModel);
            }

            try
            {
                
                var processadorEdited = await _processadorService.EditAsync(processadorModel);
                

                return Ok(processadorEdited);
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

            await _processadorService.DeleteAsync(id);
            
            return Ok();
        }

        [HttpGet("IsItemDescriptionValid/{itemDescription}/{id}")]
        public async Task<IActionResult> IsItemDescriptionValid(string itemDescription, int id)
        {
            var isValid = await this._processadorService.IsItemDescriptionValidAsync(itemDescription, id);

            return Ok(isValid);
        }
    }
}
