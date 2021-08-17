using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IProcessadorService
    {
        Task<IEnumerable<ProcessadorModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);

        Task<ProcessadorModel> GetByIdAsync(int id);
        Task<ProcessadorModel> CreateAsync(ProcessadorModel processadorModel);
        Task<ProcessadorModel> EditAsync(ProcessadorModel processadorModel);
        Task DeleteAsync(int id);

        Task<bool> IsItemDescriptionValidAsync(string itemDescription, int id);
    }
}
