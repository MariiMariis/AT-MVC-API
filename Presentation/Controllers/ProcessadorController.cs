using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

namespace Presentation.Controllers
{
    
    [Authorize]
    public class ProcessadorController : Controller
    {
        private readonly IProcessadorHttpService _processadorHttpService;
        private readonly IFabricanteHttpService _fabricanteHttpService;

        public ProcessadorController(
            IProcessadorHttpService processadorHttpService,
            IFabricanteHttpService fabricanteHttpService)

        {
            _processadorHttpService = processadorHttpService;
            _fabricanteHttpService = fabricanteHttpService;
        }

        // GET: Processador
        public async Task<IActionResult> Index(
            ProcessadorIndexViewModel processadorIndexRequest)
        {
            var processadorIndexViewModel = new ProcessadorIndexViewModel
            {
                Search = processadorIndexRequest.Search,
                OrderAscendant = processadorIndexRequest.OrderAscendant,
                Processadores = await _processadorHttpService.GetAllAsync(
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

            var processadorViewModel = await _processadorHttpService.GetByIdAsync(id.Value);

            if (processadorViewModel == null)
            {
                return NotFound();
            }

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

            var processadorCreated = await _processadorHttpService.CreateAsync(processadorViewModel);

            return RedirectToAction(nameof(Details), new { id = processadorCreated.Id });
        }

        private async Task PreencherSelectFabricantes(int? fabricanteId = null)
        {
            var fabricantes = await _fabricanteHttpService.GetAllAsync(true);

            ViewBag.Fabricantes = new SelectList(
                fabricantes,
                nameof(FabricanteViewModel.Id),
                nameof(FabricanteViewModel.NomeFabricante),
                fabricanteId);
        }

        // GET: Processador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processadorViewModel = await _processadorHttpService.GetByIdAsync(id.Value);
            if (processadorViewModel == null)
            {
                return NotFound();
            }

            await PreencherSelectFabricantes(processadorViewModel.FabricanteId);

            
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

            try
            {
                await _processadorHttpService.EditAsync(processadorViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await this.ProcessadorModelExistsAsync(processadorViewModel.Id);
                if (!exists)
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Processador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processadorViewModel = await _processadorHttpService.GetByIdAsync(id.Value);
            if (processadorViewModel == null)
            {
                return NotFound();
            }

            return View(processadorViewModel);
        }

        // POST: Processador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _processadorHttpService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProcessadorModelExistsAsync(int id)
        {
            var processador = await _processadorHttpService.GetByIdAsync(id);

            var any = processador != null;

            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsItemDescriptionValid(string itemDescription, int id)
        {
            return await _processadorHttpService.IsItemDescriptionValidAsync(itemDescription, id)
                       ? Json(true)
                       : Json($"A descrição {itemDescription} já está sendo utilizada.");
        }
    }
}

