using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            return View(await _processadorService.GetAllAsync(true));
        }

        // GET: Processador/Details/5
        public async Task<IActionResult> Details(int? id == null)
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

            return View(processadorModel);
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
        public async Task<IActionResult> Create([Bind("Id,NomeFabricante,Fundador,PaisOrigem,DataFundacao")] ProcessadorModel processadorModel)
        {
            if (!ModelState.IsValid)
            {
                PreencherSelectFabricantes(processadorModel.FabricanteId);

                return View(processadorModel);
            }

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

            await PreencherSelectFabricantes(processadorModel.FabricanteId);

            return View(processadorModel);
        }

        // POST: Processador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFabricante,Fundador,PaisOrigem,DataFundacao")] ProcessadorModel processadorModel)
        {
            if (id != processadorModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PreencherSelectFabricantes(processadorModel.FabricanteId);

                return View(processadorModel);
            }
            
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
            return this.RedirectToAction(nameof(this.Index));
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

            return View(processadorModel);
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
    }
}

