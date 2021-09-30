using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Presentation.Models;
using System.Text.Json;
using System.Runtime.Intrinsics;

namespace Presentation.Services.Implementations
{
    
    public class FabricanteHttpService : IFabricanteHttpService
    {
        private readonly HttpClient _httpClient;

        private static JsonSerializerOptions _jsonSerializerOptions = new()
                                                                          {
                                                                              IgnoreNullValues = true,
                                                                              PropertyNameCaseInsensitive = true
                                                                          };
        public FabricanteHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    
        public async Task<FabricanteViewModel> CreateAsync(FabricanteViewModel fabricanteViewModel)
        {
            var httpResponseMessage = await _httpClient
                                          .PostAsJsonAsync(string.Empty, fabricanteViewModel);

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var fabricanteCreated = await JsonSerializer
                                        .DeserializeAsync<FabricanteViewModel>(contentStream, _jsonSerializerOptions);

            return fabricanteCreated;
        }

        public async Task<IEnumerable<FabricanteViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            
            var fabricantes = await _httpClient
                            .GetFromJsonAsync<IEnumerable<FabricanteViewModel>>($"{orderAscendant}/{search}");

            return fabricantes;
        }

        public async Task<FabricanteViewModel> GetByIdAsync(int id)
        {
            var fabricantes = await _httpClient
                            .GetFromJsonAsync<FabricanteViewModel>($"{id}");

            return fabricantes;
        }


        public async Task<FabricanteViewModel> EditAsync(FabricanteViewModel fabricanteViewModel)
        {
            var httpResponseMessage = await _httpClient
                                          .PutAsJsonAsync($"{fabricanteViewModel.Id}", fabricanteViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var fabricanteEdited = await JsonSerializer
                                 .DeserializeAsync<FabricanteViewModel>(contentStream, _jsonSerializerOptions);

            return fabricanteEdited;
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponseMessage = await _httpClient
                                          .DeleteAsync($"{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<bool> IsNameValidAsync(string NomeFabricante, int id)
        {
            var IsNameValid = await _httpClient
                                  .GetFromJsonAsync<bool>($"{NomeFabricante}/{id}");

            return IsNameValid;
        }
    }
}
