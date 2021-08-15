using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;
using Data.Data;

namespace Presentation.Controllers
{
    public class FabricanteController : Controller
    {
        private readonly FabricantesContext _context;

        public FabricanteController(FabricantesContext context)
        {
            _context = context;
        }

        // GET: Fabricante
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fabricantes.ToListAsync());
        }

        // GET: Fabricante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricanteModel = await _context
                                      .Fabricantes
                                      .Include(x => x.Processadores)
                                      .FirstOrDefaultAsync(m => m.Id == id);
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
            if (ModelState.IsValid)
            {
                _context.Add(fabricanteModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fabricanteModel);
        }

        // GET: Fabricante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fabricanteModel = await _context.Fabricantes.FindAsync(id);
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
                    _context.Update(fabricanteModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FabricanteModelExists(fabricanteModel.Id))
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

            var fabricanteModel = await _context.Fabricantes
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var fabricanteModel = await _context.Fabricantes.FindAsync(id);
            _context.Fabricantes.Remove(fabricanteModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FabricanteModelExists(int id)
        {
            return _context.Fabricantes.Any(e => e.Id == id);
        }
    }
}
