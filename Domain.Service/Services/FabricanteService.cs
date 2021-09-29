using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class FabricanteService : IFabricanteService
    {
        private readonly IFabricanteRepository _fabricanteRepository;

        public FabricanteService(
            IFabricanteRepository fabricanteRepository)
        { 
            _fabricanteRepository = fabricanteRepository;
        }

        public async Task<IEnumerable<FabricanteModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _fabricanteRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<FabricanteModel> GetByIdAsync(int id)
        {
            return await _fabricanteRepository.GetByIdAsync(id);
        }

        public async Task<FabricanteModel> CreateAsync(FabricanteModel fabricanteModel)
        {
            return await _fabricanteRepository.CreateAsync(fabricanteModel);
        }

        public async Task<FabricanteModel> EditAsync(FabricanteModel fabricanteModel)
        {
            return await _fabricanteRepository.EditAsync(fabricanteModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _fabricanteRepository.DeleteAsync(id);
        }

        public async Task<bool> IsNameValidAsync(string nomeFabricante, int id)
        {
            if (string.IsNullOrWhiteSpace(nomeFabricante))
            {
                return false;
            }

            var fabricanteModel = await _fabricanteRepository.GetNameAsync(nomeFabricante, id);

            return fabricanteModel == null;
        }
    }
}
