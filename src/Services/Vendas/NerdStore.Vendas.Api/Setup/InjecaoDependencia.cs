using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Shared.Commands;
using NerdStore.Shared.Mediator;
using NerdStore.Shared.Messaging.IntegrationEvents;
using NerdStore.Vendas.Api.Application.Commands;
using NerdStore.Vendas.Api.Application.Events;
using NerdStore.Vendas.Api.Application.Queries;
using NerdStore.Vendas.Domain.Repository;
using NerdStore.Vendas.Infra.Repository;

namespace NerdStore.Vendas.Api.Setup
{
    public static class InjecaoDependencia
    {
        public static void RegistrarDependencias(this IServiceCollection services)
        {
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, RespostaPadrao>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, RespostaPadrao>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, RespostaPadrao>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, RespostaPadrao>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<IniciarPedidoCommand, RespostaPadrao>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizarPedidoCommand, RespostaPadrao>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, RespostaPadrao>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, RespostaPadrao>, PedidoCommandHandler>();

            //services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
            //services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoEvent>, PedidoEventHandler>();
            //services.AddScoped<INotificationHandler<PedidoPagamentoRealizadoEvent>, PedidoEventHandler>();
            //services.AddScoped<INotificationHandler<PedidoPagamentoRecusadoEvent>, PedidoEventHandler>();
        }
    }
}
