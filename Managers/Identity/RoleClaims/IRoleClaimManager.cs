using System.Collections.Generic;
using System.Threading.Tasks;
using modulum.Application.Requests.Identity;
using modulum.Application.Responses.Identity;
using modulum.Shared.Wrapper;

namespace modulum.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        //Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();
        //
        //Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);
        //
        //Task<IResult<string>> SaveAsync(RoleClaimRequest role);
        //
        //Task<IResult<string>> DeleteAsync(string id);
    }
}