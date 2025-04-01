namespace modulum.Client.Infrastructure.Routes
{
    public static class TokenEndpoints
    {
        public static string Get = "api/identity/token";
        public static string Refresh = "api/identity/token/refresh";
        public static string Logout = "api/identity/token/logout";
    }
}