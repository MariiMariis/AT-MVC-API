using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProcessadorRepository : IProcessadorRepository
    {
        public readonly FabricantesContext _fabricantesContext;

        public ProcessadorRepository(
            FabricantesContext fabricantesContext)
        {
            _fabricantesContext = fabricantesContext;
        }

        public async Task<IEnumerable<ProcessadorModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var processadores = orderAscendant
                                    ? _fabricantesContext.Processadores.OrderBy(x => x.NomeProcessador)
                                    : _fabricantesContext.Processadores.OrderByDescending(x => x.NomeProcessador);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await processadores
                           .Include(x => x.Fabricante)
                           .ToListAsync();
            }

            return await processadores
                       .Include(x=>x.Fabricante)
                       .Where(x => x.NomeProcessador.Contains(search))
                       .ToListAsync();
        }

        public async Task<ProcessadorModel> GetByIdAsync(int id)
        {
            var processadores = await _fabricantesContext
                                    .Processadores
                                    .Include(x => x.Fabricante)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            return processadores;
        }

        public async Task<ProcessadorModel> CreateAsync(ProcessadorModel processadorModel)
        {
            var processador = _fabricantesContext.Processadores.Add(processadorModel);

            await _fabricantesContext.SaveChangesAsync();

            return processador.Entity;

        }

        public async Task<ProcessadorModel> EditAsync(ProcessadorModel processadorModel)
        {
            var processador = _fabricantesContext.Processadores.Update(processadorModel);

            await _fabricantesContext.SaveChangesAsync();

            return processador.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var processador = await GetByIdAsync(id);

            _fabricantesContext.Processadores.Remove(processador);

            await _fabricantesContext.SaveChangesAsync();
        }

        public async Task<ProcessadorModel> GetItemDescriptionNotFromThisIdAsync(string itemDescription, int id)
        {
            var processadorModel = await _fabricantesContext
                                       .Processadores
                                       .FirstOrDefaultAsync(x => x.ItemDescription == itemDescription && x.Id != id);

            return processadorModel;
        }
    }
}
