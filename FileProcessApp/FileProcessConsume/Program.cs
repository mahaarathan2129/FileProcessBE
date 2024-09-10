using Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace FileProcessConsume
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var serviceCollections = serviceProvider.GetRequiredService<IConfiguration>();
            var consumer = new KafkaConsumer(serviceCollections);

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            string currentDir = "D:\\Projects\\FileProcessingApp\\FileProcessApp\\FileProcessConsume";
            var config = new ConfigurationBuilder()
            .SetBasePath(currentDir)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
            services.AddSingleton<IConfiguration>(config);
        }
    }
}
