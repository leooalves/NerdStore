

namespace NerdStore.Shared.Commands
{
    public class RespostaPadrao : IResposta
    {
        public RespostaPadrao(string mensagem, bool sucesso, object dados)
        {
            Mensagem = mensagem;
            Sucesso = sucesso;
            Dados = dados;
        }
        public RespostaPadrao(string mensagem, bool sucesso)
        {
            Mensagem = mensagem;
            Sucesso = sucesso;
        }

        public string Mensagem { get; private set; }
        public bool Sucesso { get; private set; }
        public object Dados { get; private set; }

    }
}
