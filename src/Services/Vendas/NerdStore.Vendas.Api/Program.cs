using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStore.Vendas.Infra.DataContext;
using Polly;
using System;

namespace NerdStore.Vendas.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<VendasContext>();


                var retry = Policy.Handle<SqlException>()
                 .WaitAndRetry(new TimeSpan[]
                 {
                             TimeSpan.FromSeconds(10),
                             TimeSpan.FromSeconds(15),
                             TimeSpan.FromSeconds(30),
                 });

                retry.Execute(() => CargaInicialVendasContext.Carregar(context));                

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
