﻿using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Shared.Messaging.IntegrationEvents;
using Polly;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using System;

namespace NerdStore.Catalogo.Api.Setup
{
    public static class MessageQueue
    {
        public static void RegisterRebus(this IServiceCollection services)
        {
            var nomeFila = "fila_rebus";
     
            services.AddRebus((configure, provider) => configure
             //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(false), nomeFila))
             //.Transport(t => t.UseRabbitMq("amqp://localhost", nomeFila)) //sem docker
             .Transport(t => t.UseRabbitMq("amqp://rabbitmq", nomeFila)) //com docker
            //.Routing(r => r.TypeBased().Map<ProdutoValorAlteradoEvent>(nomeFila))
            //.Subscriptions(s => s.StoreInMemory())             
            );


            //services.AddRebus((configure, provider) => configure
            //     //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(false), nomeFila))
            //     //.Transport(t => t.UseRabbitMq("amqp://localhost", nomeFila)) //sem docker
            //     .Transport(t => t.UseRabbitMq("amqp://rabbitmq", nomeFila))
            //     //.Routing(r => r.TypeBased().Map<ProdutoValorAlteradoEvent>(nomeFila))
            //    //.Subscriptions(s => s.StoreInMemory())             
            // ); 

            // Register handlers 
            services.AutoRegisterHandlersFromAssemblyOf<ProdutoEventHandler>();
        }
    }
}
