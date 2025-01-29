using IdentityModel.Client;

namespace MorganasTrialAPI
{
    public static class AuthService
    {
        public static async Task<string> GetAuthToken()
        {
            const string host = "https://localhost:5001";

            var client = new HttpClient();

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = $"{host}/umbraco/management/api/v1/security/back-office/token",
                    ClientId = "umbraco-back-office-admin",
                    ClientSecret = "asd123"
                }
            );

            if (tokenResponse.IsError || tokenResponse.AccessToken is null)
            {
                return string.Empty;
            }

            return tokenResponse.AccessToken;
        }
    }
}
