using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStore.Catalogo.Infra.DataContext;
using NerdStore.Catalogo.Api.Setup;
using Microsoft.Data.SqlClient;
using System;
using Polly;

namespace NerdStore.Catalogo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<CatalogoContext>();

                var retry = Policy.Handle<SqlException>()
                 .WaitAndRetry(new TimeSpan[]
                 {
                             TimeSpan.FromSeconds(10),
                             TimeSpan.FromSeconds(15),
                             TimeSpan.FromSeconds(30),
                 });

                retry.Execute(() => CargaInicialCatalogoContext.Carregar(context));

                //CargaInicialCatalogoContext.Carregar(context);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
