using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NerdStore.Shared.Messaging.IntegrationEvents;
using NerdStore.Vendas.Api.Application.Events;
using NerdStore.Vendas.Api.Setup;
using NerdStore.Vendas.Infra.DataContext;
using Rebus.ServiceProvider;

namespace NerdStore.Vendas.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.RegisterRebus();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NerdStore.Vendas.Api", Version = "v1" });
            });

            //services.AddDbContext<VendasContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<VendasContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString"));
                //options.UseSqlServer(Configuration.GetConnectionString("ConnectionStringLocal"));
            });

            services.AddMediatR(typeof(Startup));

            services.RegistrarDependencias();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NerdStore.Vendas.Api v1"));
            }

            //app.ApplicationServices.UseRebus();
                //q.Subscribe<PedidoRascunhoIniciadoEvent>().Wait();
                //q.Subscribe<PedidoItemAdicionadoEvent>().Wait();
                //q.Subscribe<PedidoEstoqueRejeitadoEvent>().Wait();
                //q.Subscribe<PedidoPagamentoRealizadoEvent>().Wait();
                //q.Subscribe<PedidoPagamentoRecusadoEvent>().Wait();
        //    });

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
