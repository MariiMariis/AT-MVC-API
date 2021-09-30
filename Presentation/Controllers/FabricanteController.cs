using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Presentation.Services;


namespace Presentation.Controllers
{
    using System;

    [Authorize]
    public class FabricanteController : Controller
    {
        private readonly IFabricanteHttpService _fabricanteHttpService;

        public FabricanteController(IFabricanteHttpService fabricanteHttpService)

        {
            _fabricanteHttpService= fabricanteHttpService;
        }

        // GET: Fabricante
        public async Task<IActionResult> Index(FabricanteIndexViewModel fabricanteIndexRequest)
        {
            
            var fabricanteIndexViewModel = new FabricanteIndexViewModel
                                               {
                                                   Search = fabricanteIndexRequest.Search,
                                                   OrderAscendant = fabricanteIndexRequest.OrderAscendant,
                                                   Fabricantes = await _fabricanteHttpService.GetAllAsync(
                                                                     fabricanteIndexRequest.OrderAscendant,
                                                                     fabricanteIndexRequest.Search)
                                               };

            return View(fabricanteIndexViewModel);
            
        }

        // GET: Fabricante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricanteViewModel = await _fabricanteHttpService.GetByIdAsync(id.Value);

            if (fabricanteViewModel == null)
            {
                return NotFound();
            }


            return View(fabricanteViewModel);
        }

        // GET: Fabricante/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FabricanteViewModel fabricanteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(fabricanteViewModel);
            }

           
            var fabricanteCreated = await _fabricanteHttpService.CreateAsync(fabricanteViewModel);

            return RedirectToAction(nameof(Details), new { id = fabricanteCreated.Id});
        }

        // GET: Fabricante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricanteViewModel = await _fabricanteHttpService.GetByIdAsync(id.Value);
            if (fabricanteViewModel == null)
            {
                return NotFound();
            }

            return View(fabricanteViewModel);
        }

        // POST: Fabricante/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FabricanteViewModel fabricanteViewModel)
        {
            if (id != fabricanteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return View(fabricanteViewModel);
            }

            try
            {
                await _fabricanteHttpService.EditAsync(fabricanteViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await FabricanteModelExistsAsync(fabricanteViewModel.Id);
                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Fabricante/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricanteViewModel = await _fabricanteHttpService.GetByIdAsync(id.Value);
            if (fabricanteViewModel == null)
            {
                return NotFound();
            }

            return View(fabricanteViewModel);
        }

        // POST: Fabricante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            await _fabricanteHttpService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FabricanteModelExistsAsync(int id)
        {
            var fabricante = await _fabricanteHttpService.GetByIdAsync(id);

            var any = fabricante != null;

            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsNameValid(string NomeFabricante, int id)
        {
            return await this._fabricanteHttpService.IsNameValidAsync(NomeFabricante, id)
                       ? Json(true)
                       : Json($"O nome {NomeFabricante} já está sendo utilizado.");
        }
    }

}
