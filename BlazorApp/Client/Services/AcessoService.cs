using Blazor.Extensions.Storage;
using BlazorApp.Client.Exceptions;
using BlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Client.Services
{
    public class AcessoService : BaseService
    {
        public AcessoService(HttpClient httpClient, LocalStorage localStorage) : base(httpClient, localStorage, "acesso")
        {
        }

        public async Task<TokenViewModel> Login(LoginViewModel entrar)
        {
            return await this.Post<TokenViewModel>(entrar);
        }
    }
}
