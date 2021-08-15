using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IFabricanteService
    {

        Task<IEnumerable<FabricanteModel>> GetAll(
            bool orderAscendant,
            string search = null);

        Task<FabricanteModel> GetById(int id);
        Task<FabricanteModel> Create(FabricanteModel fabricanteModel);
        Task<FabricanteModel> Edit(FabricanteModel fabricanteModel);
        Task Delete(int id);


    }
}
