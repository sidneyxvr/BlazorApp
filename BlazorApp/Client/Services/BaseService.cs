using BlazorApp.Client.Exceptions;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazor.Extensions.Storage;

namespace BlazorApp.Client.Services
{
    public class BaseService
    {
        private readonly HttpClient _httpClient;
        private readonly LocalStorage _localStorage;
        private readonly string urlBase;
        public BaseService(HttpClient httpClient, LocalStorage localStorage, string endpoint)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            urlBase = $"http://localhost/{endpoint}";
        }

        protected async Task<T> Post<T>(object obj, string extra = null)
        {
            var url = string.IsNullOrWhiteSpace(extra) ? urlBase : $"{urlBase}/{extra}";
            var content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpException(response.StatusCode, new List<string> { error });
        }

        protected async Task Post(object obj, string extra = null)
        {
            var url = string.IsNullOrWhiteSpace(extra) ? urlBase : $"{urlBase}/{extra}";
            var content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpException(response.StatusCode, new List<string> { error });
        }

        protected async Task Put(object obj, string extra = null)
        {
            var url = string.IsNullOrWhiteSpace(extra) ? urlBase : $"{urlBase}/{extra}";
            var content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpException(response.StatusCode, new List<string> { error });
        }

        protected async Task Delete(string extra = null)
        {
            var url = string.IsNullOrWhiteSpace(extra) ? urlBase : $"{urlBase}/{extra}";
            var response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpException(response.StatusCode, new List<string> { error });
        }

        protected async Task<T> Get<T>(string extra = null, 
            string filtroCampo = "", string filtroValor = "", string ordemCampo = "", string ordemDirecao = "")
        {
            var url = string.IsNullOrWhiteSpace(extra) ? urlBase : $"{urlBase}/{extra}/?";
            url += $"filtroCampo={filtroCampo}&filtroValor={filtroValor}&";
            url += $"filtroValor={ordemCampo}&ordemDirecao={ordemDirecao}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpException(response.StatusCode, new List<string> { error });
        }

        protected async Task<T> Get<T>(int id, string extra = null)
        {
            var url = string.IsNullOrWhiteSpace(extra) ? urlBase : $"{urlBase}/{extra}";
            var response = await _httpClient.GetAsync($"{url}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpException(response.StatusCode, new List<string> { error });
        }

        private async Task<string> GetToken()
        {
            return await _localStorage.GetItem<string>("Authorization");
        }
    }
}
