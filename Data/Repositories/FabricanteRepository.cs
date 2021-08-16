using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class FabricanteRepository : IFabricanteRepository
    {
        public readonly FabricantesContext _fabricantesContext;
        
        public FabricanteRepository(
            FabricantesContext fabricantesContext)
        {
            _fabricantesContext = fabricantesContext;
        }

        public async Task<IEnumerable<FabricanteModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var fabricantes = orderAscendant
                                  ? _fabricantesContext.Fabricantes.OrderBy(x => x.NomeFabricante)
                                  : _fabricantesContext.Fabricantes.OrderByDescending(x => x.NomeFabricante);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await fabricantes.ToListAsync();
            }

            return await fabricantes
                       .Where(x => x.NomeFabricante.Contains(search))
                       .ToListAsync();
        }

        public async Task<FabricanteModel> GetByIdAsync(int id)
        {
            var fabricante =  await _fabricantesContext
                .Fabricantes
                .Include(x => x.Processadores)
                .FirstOrDefaultAsync(x => x.Id == id);

            return fabricante;
        }

        public async Task<FabricanteModel> CreateAsync(FabricanteModel fabricanteModel)
        {
            var fabricante = _fabricantesContext.Fabricantes.Add(fabricanteModel);

             await _fabricantesContext.SaveChangesAsync();

             return fabricante.Entity;

        }

        public async Task<FabricanteModel> EditAsync(FabricanteModel fabricanteModel)
        {
            var fabricante = _fabricantesContext.Fabricantes.Update(fabricanteModel);

            await _fabricantesContext.SaveChangesAsync();

            return fabricante.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var fabricante = await GetByIdAsync(id);

            _fabricantesContext.Fabricantes.Remove(fabricante);

            await _fabricantesContext.SaveChangesAsync();
        }
    }
}
