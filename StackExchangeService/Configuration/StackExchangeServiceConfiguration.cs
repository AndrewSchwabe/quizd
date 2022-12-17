using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using StackExchangeService.Interface;

namespace StackExchangeService.Configuration
{
    public static class StackExchangeServiceConfiguration
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient("stackexchange", httpClient =>
            {
                // TODO: Configure these values
                httpClient.BaseAddress = new Uri("https://api.stackexchange.com/");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                httpClient.DefaultRequestHeaders.Add(HeaderNames.AcceptEncoding, "gzip");
            });
            serviceCollection.AddTransient<IStackExchangeService, StackExchangeService>();
        }
    }
}
