using modulum.Application.Requests.Identity;
using modulum.Shared.Wrapper;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Managers.Identity.Account
{
    public interface IAccountManager : IManager
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model);

        //Task<IResult> UpdateProfileAsync(UpdateProfileRequest model);
    }
}