using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Services.Implementations
{
    using System.Threading;

    public class FabricanteFakeService : IFabricanteHttpService
    {
        public static List<FabricanteViewModel> Fabricantes { get; } = new List<FabricanteViewModel>
                                                                           {
                                                                               new FabricanteViewModel
                                                                                   {
                                                                                       Id = 0,
                                                                                       NomeFabricante = "Intel",
                                                                                       Fundador = "George Moore",
                                                                                       PaisOrigem = "EUA",
                                                                                       DataFundacao = new DateTime(1988,06,18),
                                                                                   },

                                                                               new FabricanteViewModel
                                                                                   {
                                                                                       Id = 0,
                                                                                       NomeFabricante = "AMD",
                                                                                       Fundador = "Jerry Sanders",
                                                                                       PaisOrigem = "EUA",
                                                                                       DataFundacao = new DateTime(1990,06,01),
                                                                                   }
                                                                           };
            



        public async Task<IEnumerable<FabricanteViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            if (search == null)
            {
                return Fabricantes;
            }

            var resultByLinq = Fabricantes
                .Where(x =>
                    x.NomeFabricante.Contains(search, StringComparison.OrdinalIgnoreCase));

             resultByLinq = orderAscendant
                               ? resultByLinq.OrderBy(x => x.NomeFabricante)
                               : resultByLinq.OrderByDescending(x => x.NomeFabricante);

            return resultByLinq;
        }

        public async Task<FabricanteViewModel> GetByIdAsync(int id)
        {
            foreach (var fabricante in Fabricantes)
            {
                if (fabricante.Id == id)
                {
                    return fabricante;
                }
            }

            return null;
        }

        private static int _id = Fabricantes.Max(x => x.Id);
        private int Id => Interlocked.Increment(ref _id);

        public async Task<FabricanteViewModel> CreateAsync(FabricanteViewModel fabricanteViewModel)
        {
            fabricanteViewModel.Id = Id;

            Fabricantes.Add(fabricanteViewModel);

            return fabricanteViewModel;
        }

        public async Task<FabricanteViewModel> EditAsync(FabricanteViewModel fabricanteViewModel)
        {
            foreach (var fabricante in Fabricantes)
            {
                if (fabricante.Id == fabricanteViewModel.Id)
                {
                    fabricante.NomeFabricante = fabricanteViewModel.NomeFabricante;
                    fabricante.Fundador = fabricanteViewModel.Fundador;
                    fabricante.PaisOrigem = fabricanteViewModel.PaisOrigem;
                    fabricante.DataFundacao = fabricanteViewModel.DataFundacao;

                    return fabricante;
                }
            }
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            FabricanteViewModel usuarioEncontrado = null;
            foreach (var fabricante in Fabricantes)
            {
                if (fabricante.Id == id)
                {
                    usuarioEncontrado = fabricante;
                }
            }

            if (usuarioEncontrado != null)
            {
                Fabricantes.Remove(usuarioEncontrado);
            }
        }

        public async Task<bool> IsNameValidAsync(string NomeFabricante, int id)
        {
            throw new NotImplementedException();
        }
    }
}
