using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services
{
    public interface IFabricanteHttpService
    {
        Task<IEnumerable<FabricanteViewModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);

        Task<FabricanteViewModel> GetByIdAsync(int id);
        Task<FabricanteViewModel> CreateAsync(FabricanteViewModel fabricanteViewModel);
        Task<FabricanteViewModel> EditAsync(FabricanteViewModel fabricanteViewModel);
        Task DeleteAsync(int id);
        Task<bool> IsNameValidAsync(string NomeFabricante, int id);
    }
}
