using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MiniURL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.ConfigureAppConfiguration((hostContext, config) =>
            //{
            //    config.AddEnvironmentVariables();
            //    var buildConfig = config.Build();
            //    var azureServiceTokenProvider = new AzureServiceTokenProvider();
            //    var keyVaultClient = new KeyVaultClient((authority, resource, scope) => azureServiceTokenProvider.KeyVaultTokenCallback(authority, resource, scope));
            //    config.AddAzureKeyVault(buildConfig["AzureKeyVault:BaseUrl"], keyVaultClient, new KeyVaultManager(buildConfig["AzureKeyVault:KeyManager"]));

            //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
