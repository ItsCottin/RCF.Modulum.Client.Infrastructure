using modulum.Shared.Routes;

namespace modulum.Client.Infrastructure.Routes
{
    public static class TokenEndpoints
    {
        public static string Get = EndpointsToken.Raiz + EndpointsToken.Login;
        public static string Refresh = "api/identity/token/refresh";
        public static string Logout = EndpointsToken.Raiz + EndpointsToken.Logout;
    }
}