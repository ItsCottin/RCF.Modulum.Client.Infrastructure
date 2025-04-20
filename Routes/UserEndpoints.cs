using modulum.Shared.Routes;

namespace modulum.Client.Infrastructure.Routes
{
    public static class UserEndpoints
    {
        public static string GetAll = "api/identity/user";

        public static string Get(string userId)
        {
            return $"api/identity/user/{userId}";
        }

        public static string GetUserRoles(string userId)
        {
            return $"api/identity/user/roles/{userId}";
        }

        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string ConfirmEmail(string userId, string token)
        {
            return $"api/identity/account/confirmEmail?userId={userId}&token={token}";
        }

        public static string Export = "api/identity/user/export";
        public static string PreCadastro = EndpointsUser.Raiz + EndpointsUser.PreCadastro;
        public static string ConfirmaCadastro = EndpointsUser.Raiz + EndpointsUser.ConfirmEmail;
        public static string FinalizaCadastro = EndpointsUser.Raiz + EndpointsUser.FimCadastro;
        public static string ToggleUserStatus = "api/identity/user/toggle-status";
        public static string ForgotPassword = "api/identity/user/forgot-password";
        public static string ResetPassword = "api/identity/user/reset-password";
    }
}