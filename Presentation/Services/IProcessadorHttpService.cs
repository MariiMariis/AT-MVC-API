using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services
{
    public interface IProcessadorHttpService
    {
        Task<IEnumerable<ProcessadorViewModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);

        Task<ProcessadorViewModel> GetByIdAsync(int id);
        Task<ProcessadorViewModel> CreateAsync(ProcessadorViewModel processadorViewModel);
        Task<ProcessadorViewModel> EditAsync(ProcessadorViewModel processadorViewModel);
        Task DeleteAsync(int id);
        Task<bool> IsItemDescriptionValidAsync(string itemDescription, int id);
    }
}
