using modulum.Application.Requests.Dynamic.Create;
using modulum.Client.Infrastructure.Extensions;
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
    }
}
