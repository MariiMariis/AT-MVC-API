using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Presentation.Models;
using System.Text.Json;

namespace Presentation.Services.Implementations
{
    using System.Runtime.Intrinsics;

    public class FabricanteHttpService : IFabricanteHttpService
    {
        private readonly HttpClient _httpClient;

        private static JsonSerializerOptions _jsonSerializerOptions = new()
                                                                          {
                                                                              IgnoreNullValues = true,
                                                                              PropertyNameCaseInsensitive = true
                                                                          };
        public FabricanteHttpService()
        {
            _httpClient = new HttpClient();
            this._httpClient.BaseAddress = new Uri("https://localhost:44348/");
        }
    
        public async Task<FabricanteViewModel> CreateAsync(FabricanteViewModel fabricanteViewModel)
        {
            var httpResponseMessage = await _httpClient
                                          .PostAsJsonAsync("api/v1/FabricanteApi", fabricanteViewModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var fabricanteCreated = await JsonSerializer
                                        .DeserializeAsync<FabricanteViewModel>(contentStream, _jsonSerializerOptions);

            return fabricanteCreated;
        }

        public async Task<IEnumerable<FabricanteViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            
            var fabricantes = await _httpClient
                            .GetFromJsonAsync<IEnumerable<FabricanteViewModel>>($"/api/v1/FabricanteApi/");

            return fabricantes;
        }

        public async Task<FabricanteViewModel> GetByIdAsync(int id)
        {
            var fabricantes = await _httpClient
                            .GetFromJsonAsync<FabricanteViewModel>($"/api/v1/FabricanteApi/{id}");

            return fabricantes;
        }


        public async Task<FabricanteViewModel> EditAsync(FabricanteViewModel fabricanteViewModel)
        {
            var httpResponseMessage = await _httpClient
                                          .PutAsJsonAsync($"api/v1/FabricanteApi/{fabricanteViewModel.Id}", fabricanteViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var fabricanteEdited = await JsonSerializer
                                 .DeserializeAsync<FabricanteViewModel>(contentStream, _jsonSerializerOptions);

            return fabricanteEdited;
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponseMessage = await _httpClient
                                          .DeleteAsync($"api/v1/FabricanteApi/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<bool> IsNameValidAsync(string NomeFabricante, int id)
        {
            var IsNameValid = await _httpClient
                                  .GetFromJsonAsync<bool>($"IsNameValid/{NomeFabricante}/{id}");

            return IsNameValid;
        }
    }
}
