using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;

namespace Domain.Service.Services
{
    using Domain.Model.Models;

    public class FabricanteService : IFabricanteService
    {
        public Task<IEnumerable<FabricanteModel>> GetAll(bool orderAscendant, string search = null)
        {
            throw new NotImplementedException();
        }

        public Task<FabricanteModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FabricanteModel> Create(FabricanteModel fabricanteModel)
        {
            throw new NotImplementedException();
        }

        public Task<FabricanteModel> Edit(FabricanteModel fabricanteModel)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
