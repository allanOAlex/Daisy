using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Daisy.Client.Wasm.Handlers.AuthHandlers
{
    public class ApiAuthorizationHandler : DelegatingHandler
    {
        public ILocalStorageService localStorageService { get; set; }
        public ApiAuthorizationHandler(ILocalStorageService LocalStorageService)
        {
            localStorageService= LocalStorageService;
        }

        protected override async Task<HttpResponseMessage?> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await localStorageService.GetItemAsync<string>("authToken");
                if (token != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return await base.SendAsync(request, cancellationToken);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error|Could not send httpRequest{ex.Message}");
            }
            
        }


    }
}
