
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace MicrosoftGraphApiMail
{
    public class ClientSecretAuthProvider : IAuthenticationProvider
    {
        private IAccount _userAccount;
        private string[] _scopes;
        private IConfidentialClientApplication _msalClient;

        public ClientSecretAuthProvider(string appId,
                                        string[] scopes,
                                        string tenantId,
                                        string clientSecret)
        {
            _scopes = scopes;

            _msalClient = ConfidentialClientApplicationBuilder
                .Create(appId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .Build();
        }


        public async Task<string> GetAccessToken()
        {
            if (_userAccount == null)
            {
                try
                {
                    var resultTemp = await _msalClient
                            .AcquireTokenForClient(_scopes)
                            .ExecuteAsync();

                    _userAccount = resultTemp.Account;
                    return resultTemp.AccessToken;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Error getting access token: {exception.Message}");
                    return null;
                }
            }
            else
            {
                var resultTemp = await _msalClient
                        .AcquireTokenSilent(_scopes, _userAccount)
                        .ExecuteAsync();

                return resultTemp.AccessToken;
            }
        }


        public async Task AuthenticateRequestAsync(HttpRequestMessage requestMessage)
        {
            requestMessage.Headers.Authorization =
                new AuthenticationHeaderValue("bearer", await GetAccessToken());
        }
    }
}