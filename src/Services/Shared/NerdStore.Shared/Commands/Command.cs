

using MediatR;
using NerdStore.Shared.Commands;
using NerdStore.Shared.Entidades;
using System;

namespace NerdStore.Shared.Commands
{
    public abstract class Command : Validavel, IRequest<RespostaPadrao>
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
