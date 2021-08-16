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

        Task<IEnumerable<FabricanteModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);

        Task<FabricanteModel> GetByIdAsync(int id);
        Task<FabricanteModel> CreateAsync(FabricanteModel fabricanteModel);
        Task<FabricanteModel> EditAsync(FabricanteModel fabricanteModel);
        Task DeleteAsync(int id);

    }
}
