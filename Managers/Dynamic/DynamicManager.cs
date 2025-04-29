using modulum.Application.Requests.Dynamic;
using modulum.Application.Requests.Dynamic.Create;
using modulum.Client.Infrastructure.Extensions;
using modulum.Shared.Routes;
using modulum.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Managers.Dynamic
{
    public class DynamicManager : IDynamicManager
    {
        private readonly HttpClient _httpClient;

        public DynamicManager
            (
                HttpClient httpClient
            )
        { 
            _httpClient = httpClient;
        }
        public async Task<IResult> CadastrarDynamic(CreateDynamicTableRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.DynamicEndpoints.CadastroDynamic, request);
            return await response.ToResult();
        }

        public async Task<IResult<DynamicTableRequest>> GetAllRegistroTabela(int id)
        {
            var response = await _httpClient.GetAsync(EndpointsDynamic.Raiz + $"{id}");
            return await response.ToResult<DynamicTableRequest>();
        }

        public async Task<IResult<DynamicTableRequest>> GetNewObject(int id)
        {
            var response = await _httpClient.GetAsync(Routes.DynamicEndpoints.GetNewObject + $"/{id}");
            return await response.ToResult<DynamicTableRequest>();
        }

        public async Task<IResult<List<MenuRequest>>> GetMenu()
        {
            var response = await _httpClient.GetAsync(Routes.DynamicEndpoints.GetMenu);
            return await response.ToResult<List<MenuRequest>>();
        }

        public async Task<IResult> OperacaoRegistro(DynamicTableRequest request, string operacao) 
        {
            var response = await _httpClient.PutAsJsonAsync($"{Routes.DynamicEndpoints.CadastroDynamic}/{operacao}", request);
            return await response.ToResult();
        }
    }
}
