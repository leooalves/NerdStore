using Microsoft.Extensions.DependencyInjection;
using NerdStore.Shared.Messaging.IntegrationEvents;
using NerdStore.Vendas.Api.Application.Events;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;

namespace NerdStore.Vendas.Api.Setup
{
    public static class MessageQueue
    {
        public static void RegisterRebus(this IServiceCollection services)
        {
            var nomeFila = "fila_rebus";

            services.AddRebus((configure, provider) => configure
                 //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(false), nomeFila))
                 //.Transport(t => t.UseRabbitMq("amqp://localhost", nomeFila)) //sem docker
                 .Transport(t => t.UseRabbitMq("amqp://rabbitmq", nomeFila)) //com  docker
                 //.Routing(r => r.TypeBased().Map<ProdutoValorAlteradoEvent>(nomeFila))
                //.Subscriptions(s => s.StoreInMemory())             
             );

            // Register handlers 
            services.AutoRegisterHandlersFromAssemblyOf<PedidoEventHandler>();
        }
    }
}
