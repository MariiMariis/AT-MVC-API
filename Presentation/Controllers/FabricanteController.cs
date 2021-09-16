using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Interfaces.Services;
using Presentation.Models;
using Microsoft.AspNetCore.Authorization;


namespace Presentation.Controllers
{
    
    [Authorize]
    public class FabricanteController : Controller
    {
        private readonly IFabricanteService _fabricanteService;

        public FabricanteController(
            IFabricanteService fabricanteService)

        {
            _fabricanteService = fabricanteService;
        }

        // GET: Fabricante
        public async Task<IActionResult> Index(FabricanteIndexViewModel fabricanteIndexRequest)
        {
            var fabricanteIndexViewModel = new FabricanteIndexViewModel
            {
                Search = fabricanteIndexRequest.Search,
                OrderAscendant = fabricanteIndexRequest.OrderAscendant,
                Fabricantes = await _fabricanteService.GetAllAsync(
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

            var fabricanteModel = await _fabricanteService.GetByIdAsync(id.Value);

            if (fabricanteModel == null)
            {
                return NotFound();
            }

            var fabricanteViewModel = FabricanteViewModel.From(fabricanteModel);

            return View(fabricanteViewModel);
        }

        // GET: Fabricante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fabricante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FabricanteViewModel fabricanteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(fabricanteViewModel);
            }

            var fabricanteModel = fabricanteViewModel.ToModel();
            var fabricanteCreated = await _fabricanteService.CreateAsync(fabricanteModel);

            return RedirectToAction(nameof(Details), new { id = fabricanteCreated.Id});
        }

        // GET: Fabricante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricanteModel = await _fabricanteService.GetByIdAsync(id.Value);
            if (fabricanteModel == null)
            {
                return NotFound();
            }
            
            var fabricanteViewModel = FabricanteViewModel.From(fabricanteModel);

            return View(fabricanteViewModel);
        }

        // POST: Fabricante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            var fabricanteModel = fabricanteViewModel.ToModel();
            try
            {
                await _fabricanteService.EditAsync(fabricanteModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await FabricanteModelExistsAsync(fabricanteModel.Id);
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

            var fabricanteModel = await _fabricanteService.GetByIdAsync(id.Value);
            if (fabricanteModel == null)
            {
                return NotFound();
            }

            var fabricanteViewModel = FabricanteViewModel.From(fabricanteModel);
            return View(fabricanteViewModel);
        }

        // POST: Fabricante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            await _fabricanteService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FabricanteModelExistsAsync(int id)
        {
            var fabricante = await _fabricanteService.GetByIdAsync(id);

            var any = fabricante != null;

            return any;
        }
    }
}
