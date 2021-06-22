using NerdStore.Shared.Entidades.DTO;
using System.Threading.Tasks;

namespace NerdStore.Pagamentos.Business
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
    }
}