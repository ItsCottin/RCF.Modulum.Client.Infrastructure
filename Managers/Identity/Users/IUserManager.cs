using modulum.Application.Requests.Identity;
using modulum.Application.Responses.Identity;
using modulum.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Managers.Identity.Users
{
    public interface IUserManager : IManager
    {
        Task<IResult<List<UserResponse>>> GetAllAsync();

        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);

        Task<IResult<UserResponse>> GetAsync(string userId);

        Task<IResult<UserRolesResponse>> GetRolesAsync(string userId);

        Task<IResult> RegisterUserAsync(RegisterRequest request);

        Task<IResult> ConfirmEmail(string userId, string token);

        //Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);

        //Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request);
    }
}