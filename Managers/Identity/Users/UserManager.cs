using Blazored.LocalStorage;
using modulum.Application.Requests.Account;
using modulum.Application.Requests.Identity;
using modulum.Application.Responses.Identity;
using modulum.Client.Infrastructure.Extensions;
using modulum.Shared.Constants.Storage;
using modulum.Shared.Wrapper;
using Newtonsoft.Json.Linq;
using nodulum.Application.Requests.Identity;
using System.Collections.Generic;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Managers.Identity.Users
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserManager
            (
            HttpClient httpClient,
            ILocalStorageService localStorage
            )
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<IResult<List<UserResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.UserEndpoints.GetAll);
            return await response.ToResult<List<UserResponse>>();
        }

        public async Task<IResult<UserResponse>> GetAsync(string userId)
        {
            var response = await _httpClient.GetAsync(Routes.UserEndpoints.Get(userId));
            return await response.ToResult<UserResponse>();
        }

        public async Task<IResult> PreRegisterUserAsync(PreRegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.PreCadastro, request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.ToResult();
                if (result.Succeeded)
                {
                    await _localStorage.SetItemAsync(StorageConstants.Local.EmailCadastro, request.Email);
                    if (result.Fields.TryGetValue("CodeTwoFactor", out var code))
                    {
                        await _localStorage.SetItemAsync(StorageConstants.Local.CodeTwoFactor, code);
                    }
                }
            }
            return await response.ToResult();
        }

        //public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
        //{
        //    var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.ToggleUserStatus, request);
        //    return await response.ToResult();
        //}

        public async Task<IResult<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var response = await _httpClient.GetAsync(Routes.UserEndpoints.GetUserRoles(userId));
            return await response.ToResult<UserRolesResponse>();
        }

        //public async Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        //{
        //    var response = await _httpClient.PutAsJsonAsync(Routes.UserEndpoints.GetUserRoles(request.UserId), request);
        //    return await response.ToResult<UserRolesResponse>();
        //}

        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.ForgotPassword, model);
            return await response.ToResult();
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.ResetPassword, request);
            return await response.ToResult();
        }

        public async Task<IResult> ConfirmEmail(string userId, string token)
        {
            var response = await _httpClient.GetAsync(Routes.UserEndpoints.ConfirmEmail(userId, token));
            return await response.ToResult();
        }

        public async Task<IResult> ConfirmEmail(TwoFactorRequest request)
        { 
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.ConfirmaCadastro, request);
            return await response.ToResult();
        }

        public async Task<IResult> FimRegisterUserAsync(FinishRegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.FinalizaCadastro, request);
            return await response.ToResult();
        }

        public async Task<string> GetItemLocalStorage(string key)
        { 
            string? value = await _localStorage.GetItemAsync<string>(key);
            if (string.IsNullOrEmpty(value))
                return "";

            return value;
        }
    }
}