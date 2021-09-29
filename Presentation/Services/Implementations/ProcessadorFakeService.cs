using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class ProcessadorFakeService : IProcessadorHttpService
    {
        private static List<ProcessadorViewModel> Processadores { get; } = new List<ProcessadorViewModel> 
                                                                               {
                                                                                   new ProcessadorViewModel()
                                                                                       {
                                                                                            Id = 0,
                                                                                            NomeProcessador = "i9",
                                                                                            ItemDescription = "9900",
                                                                                            Cores = 4,
                                                                                            Threads = 8,
                                                                                            BaseFrequency = 3.4,
                                                                                            LaunchDate = DateTime.Now,
                                                                                       },
                                                                                   new ProcessadorViewModel()
                                                                                       {
                                                                                           Id = 1,
                                                                                           NomeProcessador = "i9",
                                                                                           ItemDescription = "9800",
                                                                                           Cores = 4,
                                                                                           Threads = 8,
                                                                                           BaseFrequency = 3.2,
                                                                                           LaunchDate = DateTime.Now,
                                                                                       }
                                                                               };

        public async Task<IEnumerable<ProcessadorViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            if (search == null)
            {
                return Processadores;
            }

            var resultByLinq = Processadores
                .Where(x => x.NomeProcessador.Contains(search, StringComparison.OrdinalIgnoreCase));

            resultByLinq = orderAscendant
                               ? resultByLinq.OrderBy(x => x.NomeProcessador)
                               : resultByLinq.OrderByDescending(x => x.NomeProcessador);

            return resultByLinq;
        }

        public async Task<ProcessadorViewModel> GetByIdAsync(int id)
        {
            foreach (var processadorViewModel  in Processadores)
            {
                if (processadorViewModel.Id == id)
                {
                    return processadorViewModel;
                }
            }

            return null;
        }

        private static int _id = Processadores.Max(x => x.Id);
        private int Id => Interlocked.Increment(ref _id);

        public async Task<ProcessadorViewModel> CreateAsync(ProcessadorViewModel processadorViewModel)
        {
            processadorViewModel.Id = Id;

            Processadores.Add(processadorViewModel);

            return processadorViewModel;
        }

        public async Task<ProcessadorViewModel> EditAsync(ProcessadorViewModel processadorViewModel)
        {
            foreach (var processador in Processadores)
            {
                if (processador.Id == processadorViewModel.Id)
                {
                    processador.NomeProcessador = processadorViewModel.NomeProcessador;
                    processador.ItemDescription = processadorViewModel.ItemDescription;
                    processador.Cores = processadorViewModel.Cores;
                    processador.Threads = processadorViewModel.Threads;
                    processador.BaseFrequency = processadorViewModel.BaseFrequency;
                    processador.LaunchDate = processadorViewModel.LaunchDate;
                    processador.FabricanteId = processadorViewModel.FabricanteId;

                    return processador;
                }
            }

            return null;
        }

        public Task DeleteAsync(int id)
        {
            ProcessadorViewModel processadorViewModel = null;
            foreach (var processador in Processadores)
            {
                if (processador.Id == id)
                {
                    processadorViewModel = processador;
                }
            }

            if (processadorViewModel != null)
            {
                Processadores.Remove(processadorViewModel);
            }
        }

        public Task<bool> IsItemDescriptionValidAsync(string itemDescription, int id)
        {
            throw new NotImplementedException();
        }
    }
}
