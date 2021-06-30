using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NerdStore.Catalogo.Api.Setup;
using NerdStore.Catalogo.Application.AutoMapper;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Infra.DataContext;
using NerdStore.Shared.Messaging;
using NerdStore.Shared.Messaging.IntegrationEvents;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;

namespace NerdStore.Catalogo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {

            var nomeFila = "fila_rebus";

            services.AddRebus((configure, provider) => configure
             //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(false), nomeFila))             
             //.Transport(t => t.UseRabbitMq(Configuration["Rabbitmq"], nomeFila)) //com docker
             .Transport(t => t.UseRabbitMq(Configuration["RabbitConnection"], nomeFila)) //com docker             
             .Routing(r => r.TypeBased()
                //.MapAssemblyOf<Message>(nomeFila)
                //.MapAssemblyOf<ProdutoValorAlteradoEvent>(nomeFila)
                //.MapAssemblyOf<PedidoEstoqueRejeitadoEvent>(nomeFila)
                //.MapAssemblyOf<PedidoEstoqueConfirmadoEvent>(nomeFila)
                )
            //.Subscriptions(s => s.StoreInMemory())             
            );

            // Register handlers 
            services.AutoRegisterHandlersFromAssemblyOf<ProdutoEventHandler>();

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NerdStore.Catalogo.Api", Version = "v1" });
            });

            services.AddMediatR(typeof(Startup));

            services.AddAutoMapper(typeof(DomainToViewModelProfile), typeof(ViewModelToDomainProfile));

            //services.AddDbContext<VendasContext>(opt => opt.UseInMemoryDatabase("Database"));      
            services.AddDbContext<CatalogoContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"]);                 
            });

            services.RegistrarDependencias();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.ApplicationServices.UseRebus(q => {
                //q.Subscribe<ProdutoValorAlteradoEvent>().Wait();
                //q.Subscribe<PedidoIniciadoEvent>().Wait();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NerdStore.Catalogo.Api v1"));

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
