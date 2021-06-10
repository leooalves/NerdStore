
using System;

namespace NerdStore.Shared.Entidades
{
    public abstract class Entity : Validavel
    {
        public Guid Id { get; private set; }        

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
      
    }
}
