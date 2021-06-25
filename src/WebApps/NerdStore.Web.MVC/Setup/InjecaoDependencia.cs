using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Domain.Repository;
using NerdStore.Catalogo.Infra.Repository;
using NerdStore.Shared.Mediator;

namespace NerdStore.Web.MVC.Setup
{
    public static class InjecaoDependencia
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //meadiatr
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            //catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();            
        }
    }
}
