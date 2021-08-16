using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Domain.Model.Interfaces.Services;

namespace Domain.Service.Services
{
    public class ProcessadorService : IProcessadorService
    {
        private readonly IProcessadorRepository _processadorRepository;

        public ProcessadorService(
            IProcessadorRepository processadorRepository)
        {
            _processadorRepository = processadorRepository;
        }

        public async Task<IEnumerable<ProcessadorModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _processadorRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<ProcessadorModel> GetByIdAsync(int id)
        {
            return await _processadorRepository.GetByIdAsync(id);
        }

        public async Task<ProcessadorModel> CreateAsync(ProcessadorModel processadorModel)
        {
            return await _processadorRepository.CreateAsync(processadorModel);
        }

        public async Task<ProcessadorModel> EditAsync(ProcessadorModel processadorModel)
        {
            return await _processadorRepository.EditAsync(processadorModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _processadorRepository.DeleteAsync(id);
        }
    }
}
