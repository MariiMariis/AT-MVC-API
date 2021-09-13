using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IFabricanteRepository
    {
        Task<IEnumerable<FabricanteModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);

        Task <FabricanteModel> GetByIdAsync(int id);
        Task <FabricanteModel> CreateAsync(FabricanteModel fabricanteModel);
        Task <FabricanteModel> EditAsync(FabricanteModel fabricanteModel);
        Task DeleteAsync(int id);
    }
}
