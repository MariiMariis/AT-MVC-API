using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IProcessadorRepository
    {
        Task<IEnumerable<ProcessadorModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);

        Task<ProcessadorModel> GetByIdAsync(int id);
        Task<ProcessadorModel> CreateAsync(ProcessadorModel processadorModel);
        Task<ProcessadorModel> EditAsync(ProcessadorModel processadorModel);
        Task DeleteAsync(int id);
        Task <ProcessadorModel>GetItemDescriptionNotFromThisIdAsync(string itemDescription, int id);
    }
}
