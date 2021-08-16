using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;
using Data.Data;
using Domain.Model.Interfaces.Services;

namespace Presentation.Controllers
{
    public class FabricanteController : Controller
    {
        private readonly FabricantesContext _context;
        private readonly IFabricanteService _fabricanteService;

        public FabricanteController(
            FabricantesContext context,
            IFabricanteService fabricanteService)

        {
            _context = context;
            _fabricanteService = fabricanteService;
        }

        // GET: Fabricante
        public async Task<IActionResult> Index()
        {
            return View(await _fabricanteService.GetAllAsync(true));
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

            return View(fabricanteModel);
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
        public async Task<IActionResult> Create([Bind("Id,NomeFabricante,Fundador,PaisOrigem,DataFundacao")] FabricanteModel fabricanteModel)
        {
            if (!ModelState.IsValid)
            {
                return View(fabricanteModel);
            }

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
            return View(fabricanteModel);
        }

        // POST: Fabricante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFabricante,Fundador,PaisOrigem,DataFundacao")] FabricanteModel fabricanteModel)
        {
            if (id != fabricanteModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(fabricanteModel);
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

            return View(fabricanteModel);
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
