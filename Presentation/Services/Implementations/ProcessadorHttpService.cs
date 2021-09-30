using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Presentation.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Presentation.Services.Implementations
{
    public class ProcessadorHttpService : IProcessadorHttpService
    {

        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
                                                                                  {
                                                                                      IgnoreNullValues = true,
                                                                                      PropertyNameCaseInsensitive = true
                                                                                  };

        public ProcessadorHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<ProcessadorViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var processadores = await _httpClient
                                .GetFromJsonAsync<IEnumerable<ProcessadorViewModel>>($"{orderAscendant}/{search}");

            return processadores;
        }

        public async Task<ProcessadorViewModel> GetByIdAsync(int id)
        {
            var processadores = await _httpClient
                                    .GetFromJsonAsync<ProcessadorViewModel>($"{id}");

            return processadores;
        }

        public async Task<ProcessadorViewModel> CreateAsync(ProcessadorViewModel processadorViewModel)
        {
            var httpResponseMessage = await _httpClient
                                          .PostAsJsonAsync(string.Empty, processadorViewModel);

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var processadorCreate = await JsonSerializer
                                      .DeserializeAsync<ProcessadorViewModel>(contentStream, JsonSerializerOptions);
            return processadorCreate;
        }

        public async Task<ProcessadorViewModel> EditAsync(ProcessadorViewModel processadorViewModel)
        {
            var httpResponseMessage = await _httpClient
                                          .PutAsJsonAsync($"{processadorViewModel.Id}", processadorViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var processadorEdited = await JsonSerializer
                                     .DeserializeAsync<ProcessadorViewModel>(contentStream, JsonSerializerOptions);

            return processadorEdited;
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponseMessage = await _httpClient
                                          .DeleteAsync($"{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<bool> IsItemDescriptionValidAsync(string itemDescription, int id)
        {
            var isItemDescriptionValid = await _httpClient
                                     .GetFromJsonAsync<bool>($"IsItemDescriptionValid/{itemDescription}/{id}");

            return isItemDescriptionValid;
        }
    }
}
