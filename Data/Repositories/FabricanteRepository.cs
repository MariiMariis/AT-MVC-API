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
            if (string.IsNullOrWhiteSpace(search))
            {
                return await _fabricantesContext.Fabricantes.ToListAsync();
            }
            return await _fabricantesContext
                       .Fabricantes
                       .Where(x => x.NomeFabricante.Contains( search))
                       .ToListAsync();
        }

        public async Task<FabricanteModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FabricanteModel> CreateAsync(FabricanteModel fabricanteModel)
        {
            throw new NotImplementedException();
        }

        public async Task<FabricanteModel> EditAsync(FabricanteModel fabricanteModel)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
