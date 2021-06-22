
using NerdStore.Shared.Messaging;
using System;
using System.Collections.Generic;

namespace NerdStore.Shared.Entidades
{
    public abstract class Entity : Validavel
    {
        public Guid Id { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        private List<Event> _eventos;
        public IReadOnlyCollection<Event> Eventos => _eventos?.AsReadOnly();

        public void AdicionarEvento(Event evento)
        {
            _eventos = _eventos ?? new List<Event>();
            _eventos.Add(evento);
        }
        public void LimparEventos()
        {
            _eventos?.Clear();
        }

    }
}
