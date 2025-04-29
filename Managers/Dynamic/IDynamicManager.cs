using modulum.Application.Requests.Dynamic;
using modulum.Application.Requests.Dynamic.Create;
using modulum.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Managers.Dynamic
{
    public interface IDynamicManager : IManager
    {
        Task<IResult> CadastrarDynamic(CreateDynamicTableRequest request);

        Task<IResult<DynamicTableRequest>> GetAllRegistroTabela(int id);

        Task<IResult<List<MenuRequest>>> GetMenu();

        Task<IResult> OperacaoRegistro(DynamicTableRequest request, string operacao);

        Task<IResult<DynamicTableRequest>> GetNewObject(int id);
    }
}
