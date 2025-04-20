using Blazored.LocalStorage;
using modulum.Application.Requests.Identity;
using modulum.Application.Responses.Identity;
using modulum.Client.Infrastructure.Authentication;
using modulum.Client.Infrastructure.Extensions;
using modulum.Client.Infrastructure.Routes;
using modulum.Shared.Constants.Storage;
using modulum.Shared.Wrapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using modulum.Shared.Routes;

namespace modulum.Client.Infrastructure.Managers.Identity.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private bool _authenticated = false;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;


        public AuthenticationManager(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ClaimsPrincipal> CurrentUser()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return state.User;
        }

        public async Task<IResult> Login(TokenRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.Get, model);
            var result = await response.ToResult<TokenResponse>();
            if (result.Succeeded)
            {
                var token = result.Data.Token;
                var refreshToken = result.Data.RefreshToken;
                await _localStorage.SetItemAsync(StorageConstants.Local.AuthToken, token);
                await _localStorage.SetItemAsync(StorageConstants.Local.RefreshToken, refreshToken);

                await ((BlazorHeroStateProvider)this._authenticationStateProvider).StateChangedAsync();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await Result.SuccessAsync();
            }
            else
            {
                return await Result.FailAsync(result.Messages, result.Fields);
            }
        }

        public async Task<IResult> Logout()
        {
            const string Empty = "{}";
            var emptyContent = new StringContent(Empty, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(TokenEndpoints.Logout, emptyContent);
            if (result.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemAsync(StorageConstants.Local.AuthToken);
                await _localStorage.RemoveItemAsync(StorageConstants.Local.RefreshToken);
                await _localStorage.RemoveItemAsync(StorageConstants.Local.UserImageURL);
                ((BlazorHeroStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
            await ((BlazorHeroStateProvider)this._authenticationStateProvider).StateChangedAsync(); // 4 horas de pesquisa e testes pra descobrir que no final era só adicionar essa linha
            return await Result.SuccessAsync();
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken);
            var refreshToken = await _localStorage.GetItemAsync<string>(StorageConstants.Local.RefreshToken);

            var response = await _httpClient.PostAsJsonAsync(Routes.TokenEndpoints.Refresh, new RefreshTokenRequest { Token = token, RefreshToken = refreshToken });

            var result = await response.ToResult<TokenResponse>();

            if (!result.Succeeded)
            {
                throw new ApplicationException("Something went wrong during the refresh token action");
            }

            token = result.Data.Token;
            refreshToken = result.Data.RefreshToken;
            await _localStorage.SetItemAsync(StorageConstants.Local.AuthToken, token);
            await _localStorage.SetItemAsync(StorageConstants.Local.RefreshToken, refreshToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return token;
        }

        public async Task<string> TryRefreshToken()
        {
            //check if token exists
            var availableToken = await _localStorage.GetItemAsync<string>(StorageConstants.Local.RefreshToken);
            if (string.IsNullOrEmpty(availableToken)) return string.Empty;
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var exp = user.FindFirst(c => c.Type.Equals("exp"))?.Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
            var timeUTC = DateTime.UtcNow;
            var diff = expTime - timeUTC;
            if (diff.TotalMinutes <= 1)
                return await RefreshToken();
            return string.Empty;
        }

        public async Task<string> TryForceRefreshToken()
        {
            return await RefreshToken();
        }

        public async Task<bool> CheckAuthenticatedAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity.IsAuthenticated == null)
            {
                return false;
            }
            return authState.User.Identity.IsAuthenticated;
        }
    }
}