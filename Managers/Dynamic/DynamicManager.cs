using modulum.Application.Requests.Dynamic;
using modulum.Application.Requests.Dynamic.Create;
using modulum.Application.Requests.Dynamic.Relationship;
using modulum.Application.Requests.Dynamic.Update;
using modulum.Application.Responses.Dynamic;
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
        public async Task<IResult> OperacaoMapTable(CreateDynamicTableRequest request, string operacao)
        {
            if (operacao.Equals("create"))
            {
                var response = await _httpClient.PostAsJsonAsync(Routes.DynamicEndpoints.CadastroDynamic, request);
                return await response.ToResult();
            }
            else
            {
                var response = await _httpClient.PostAsJsonAsync(Routes.DynamicEndpoints.AlterMapTable, request);
                return await response.ToResult();
            }
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
            var response = await _httpClient.PutAsJsonAsync($"{Routes.DynamicEndpoints.CadastroDynamic}{operacao}", request);
            return await response.ToResult();
        }

        public async Task<IResult> DeletePorIdAsync(DynamicForIdRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.DynamicEndpoints.DeleteDynamicById, request);
            return await response.ToResult();
        }

        public async Task<IResult<DynamicTableRequest>> GetRegistroPorIdTabelaEIdRegistro(DynamicForIdRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.DynamicEndpoints.SelectDynamicById, request);
            return await response.ToResult<DynamicTableRequest>();
        }

        public async Task<IResult<CreateDynamicTableRequest>> GetMapTable(int idTable)
        {
            var response = await _httpClient.GetAsync(Routes.DynamicEndpoints.GetMapTable + $"/{idTable}");
            return await response.ToResult<CreateDynamicTableRequest>();
        }

        public async Task<IResult> AlterarRelacionamento(List<CreateDynamicRelationshipRequest> request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.DynamicEndpoints.AlterRelacionamento, request);
            return await response.ToResult();
        }

        public async Task<IResult> DeletarRelacionamento(DynamicForIdRequest request) 
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.DynamicEndpoints.DeletarRelacionamento, request);
            return await response.ToResult();
        }

        public async Task<IResult<List<CreateDynamicRelationshipRequest>>> ConsultarRelacionamento(int idTable)
        {
            var response = await _httpClient.GetAsync(Routes.DynamicEndpoints.ConsultarRelacionamento + $"/{idTable}");
            return await response.ToResult<List<CreateDynamicRelationshipRequest>>();
        }

        public async Task<IResult> RenameNomeTabelaTela(RenameNomeTabelaTelaRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.DynamicEndpoints.RenameNomeTabelaTela, request);
            return await response.ToResult();
        }

        public async Task<IResult> DeleteMapTableAsync(int idTable)
        {
            var response = await _httpClient.DeleteAsync(Routes.DynamicEndpoints.DeleteMapTable + $"/{idTable}");
            return await response.ToResult();
        }
    }
}
