using System;
using Flunt.Notifications;

namespace GestaoDeUsuarios.Domain.Base 
{
    public abstract class Entitie : Notifiable
    {
        protected Entitie() => Id = Guid.NewGuid();

        public Guid Id { get; private set; }
    }
}
