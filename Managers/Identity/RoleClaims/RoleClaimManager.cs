﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using modulum.Application.Requests.Identity;
using modulum.Application.Responses.Identity;
using modulum.Client.Infrastructure.Extensions;
using modulum.Shared.Wrapper;

namespace modulum.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public class RoleClaimManager : IRoleClaimManager
    {
        private readonly HttpClient _httpClient;

        public RoleClaimManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<IResult<string>> DeleteAsync(string id)
        //{
        //    var response = await _httpClient.DeleteAsync($"{Routes.RoleClaimsEndpoints.Delete}/{id}");
        //    return await response.ToResult<string>();
        //}
        //
        //public async Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync()
        //{
        //    var response = await _httpClient.GetAsync(Routes.RoleClaimsEndpoints.GetAll);
        //    return await response.ToResult<List<RoleClaimResponse>>();
        //}
        //
        //public async Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId)
        //{
        //    var response = await _httpClient.GetAsync($"{Routes.RoleClaimsEndpoints.GetAll}/{roleId}");
        //    return await response.ToResult<List<RoleClaimResponse>>();
        //}
        //
        //public async Task<IResult<string>> SaveAsync(RoleClaimRequest role)
        //{
        //    var response = await _httpClient.PostAsJsonAsync(Routes.RoleClaimsEndpoints.Save, role);
        //    return await response.ToResult<string>();
        //}
    }
}