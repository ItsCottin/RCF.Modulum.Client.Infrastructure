using modulum.Application.Requests.Dynamic;
using modulum.Application.Requests.Dynamic.Create;
using modulum.Application.Requests.Dynamic.Relationship;
using modulum.Application.Requests.Dynamic.Update;
using modulum.Application.Responses.Dynamic;
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
        Task<IResult> OperacaoMapTable(CreateDynamicTableRequest request, string operacao);
        Task<IResult<DynamicTableRequest>> GetAllRegistroTabela(int id);
        Task<IResult<List<MenuRequest>>> GetMenu();
        Task<IResult> OperacaoRegistro(DynamicTableRequest request, string operacao);
        Task<IResult<DynamicTableRequest>> GetNewObject(int id);
        Task<IResult> DeletePorIdAsync(DynamicForIdRequest request);
        Task<IResult<DynamicTableRequest>> GetRegistroPorIdTabelaEIdRegistro(DynamicForIdRequest request);
        Task<IResult<CreateDynamicTableRequest>> GetMapTable(int idTable);
        Task<IResult> AlterarRelacionamento(List<CreateDynamicRelationshipRequest> request);
        Task<IResult> DeletarRelacionamento(DynamicForIdRequest request);
        Task<IResult<List<CreateDynamicRelationshipRequest>>> ConsultarRelacionamento(int idTable);
        Task<IResult> RenameNomeTabelaTela(RenameNomeTabelaTelaRequest request);
        Task<IResult> DeleteMapTableAsync(int idTable);
    }
}
