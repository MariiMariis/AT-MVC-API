using System.Collections.Generic;
using System.Linq;
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
            var fabricantes = _fabricantesContext.Fabricantes.AsQueryable();

            if (string.IsNullOrWhiteSpace(search))
            {
                fabricantes = fabricantes
                    .Where(x => x.NomeFabricante.Contains(search));
            }

            fabricantes = orderAscendant
                        ? fabricantes.OrderBy(x => x.NomeFabricante)
                        : fabricantes.OrderByDescending(x => x.NomeFabricante);

            return fabricantes;

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

        public async Task<FabricanteModel> GetNameAsync(string nomeFabricante, int id)
        {
            var fabricanteModel = await _fabricantesContext
                                .Fabricantes
                                .FirstOrDefaultAsync(x => x.NomeFabricante == nomeFabricante && x.Id != id);

            return fabricanteModel;

        }
    }
}
