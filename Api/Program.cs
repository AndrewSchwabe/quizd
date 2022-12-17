using Microsoft.Extensions.Hosting;
using StackExchangeService.Configuration;

namespace ApiIsolated
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices((httpContext, serviceCollection) => 
                {
                    StackExchangeServiceConfiguration.Configure(serviceCollection);
                })
                .Build();

            host.Run();
        }
    }
}