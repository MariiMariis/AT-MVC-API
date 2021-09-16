using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Microsoft.AspNetCore.Authorization;


namespace Presentation.Controllers
{
    
    [Authorize]
    public class ProcessadorController : Controller
    {
        private readonly IProcessadorService _processadorService;
        private readonly IFabricanteService _fabricanteService;

        public ProcessadorController(
            IProcessadorService processadorService,
            IFabricanteService fabricanteService)

        {
            _processadorService = processadorService;
            _fabricanteService = fabricanteService;
        }

        // GET: Processador
        public async Task<IActionResult> Index(
            ProcessadorIndexViewModel processadorIndexRequest)
        {
            var processadorIndexViewModel = new ProcessadorIndexViewModel
            {
                Search = processadorIndexRequest.Search,
                OrderAscendant = processadorIndexRequest.OrderAscendant,
                Processadores = await _processadorService.GetAllAsync(
                                    processadorIndexRequest.OrderAscendant,
                                    processadorIndexRequest.Search)
                                   
            };

            return View(processadorIndexViewModel);

        }

        // GET: Processador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processadorModel = await _processadorService.GetByIdAsync(id.Value);

            if (processadorModel == null)
            {
                return NotFound();
            }

            var processadorViewModel = ProcessadorViewModel.From(processadorModel);
            return View(processadorViewModel);
        }

        // GET: Processador/Create
        public async Task<IActionResult> Create()
        {
            await PreencherSelectFabricantes();

            return View();
        }

        // POST: Processador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcessadorViewModel processadorViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherSelectFabricantes(processadorViewModel.FabricanteId);

                return View(processadorViewModel);
            }

            var processadorModel = processadorViewModel.ToModel();
            var processadorCreated = await _processadorService.CreateAsync(processadorModel);

            return RedirectToAction(nameof(Details), new { id = processadorCreated.Id });
        }

        private async Task PreencherSelectFabricantes(int? fabricanteId = null)
        {
            var fabricantes = await _fabricanteService.GetAllAsync(true);

            ViewBag.Fabricantes = new SelectList(
                fabricantes,
                nameof(FabricanteModel.Id),
                nameof(FabricanteModel.NomeFabricante),
                fabricanteId);
        }

        // GET: Processador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processadorModel = await _processadorService.GetByIdAsync(id.Value);
            if (processadorModel == null)
            {
                return NotFound();
            }

            await PreencherSelectFabricantes(processadorModel.FabricanteModelId);

            var processadorViewModel = ProcessadorViewModel.From(processadorModel);
            return View(processadorViewModel);
        }

        // POST: Processador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProcessadorViewModel processadorViewModel)
        {
            if (id != processadorViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PreencherSelectFabricantes(processadorViewModel.FabricanteId);

                return View(processadorViewModel);
            }

            var processadorModel = processadorViewModel.ToModel();
            try
            {
                await _processadorService.EditAsync(processadorModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await this.ProcessadorModelExistsAsync(processadorModel.Id);
                if (!exists)
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.RedirectToAction(nameof(Index));
        }

        // GET: Processador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processadorModel = await _processadorService.GetByIdAsync(id.Value);
            if (processadorModel == null)
            {
                return NotFound();
            }

            var processadorViewModel = ProcessadorViewModel.From(processadorModel);
            return View(processadorViewModel);
        }

        // POST: Processador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _processadorService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProcessadorModelExistsAsync(int id)
        {
            var processador = await _processadorService.GetByIdAsync(id);

            var any = processador != null;

            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsItemDescriptionValid(string itemDescription, int id)
        {
            return await _processadorService.IsItemDescriptionValidAsync(itemDescription, id)
                       ? Json(true)
                       : Json($"A descrição {itemDescription} já está sendo utilizada.");
        }
    }
}

