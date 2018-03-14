using System;
using System.IO;
using CatsConsumer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatsConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var servicesProvider = ConfigureServices();
                var appRunner = servicesProvider.GetRequiredService<IAppRunner>();

                appRunner.RunAsync().Wait();
            }
            catch (AggregateException ae)
            { 
                foreach(var innerException in ae.Flatten().InnerExceptions)
                {
                    PrintException(innerException);
                }
            }
            catch (Exception e)
            {
                PrintException(e);
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Hit Enter to exit.");
            Console.Read();
        }

        private static void PrintException(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            do
            {
                Console.WriteLine(e.Message);
                e = e.InnerException;
            }
            while (e != null);

            Console.ResetColor();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddTransient<IAppRunner, AppRunner>();
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddTransient<ICatsByOwnerGenderMapper, CatsByOwnerGenderMapper>();
            services.AddTransient<ICatsByOwnerGenderWriter, CatsByOwnerGenderWriter>();

            // IHttpClient objects are instantiated as singleton instead of as transient so that TCP
            // connections are used efficiently and an issue with sockets will not occur.
            // ref. https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/ 
            services.AddSingleton<IHttpClient, HttpClientWrapper>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
