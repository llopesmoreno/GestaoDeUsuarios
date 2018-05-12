using System;
using Flunt.Validations;
using Flunt.Notifications;
using GestaoDeUsuarios.Domain.Base.Commands;

namespace GestaoDeUsuarios.Domain.Commands
{
    public class DeleteUserCommand : Notifiable, ICommand
    {
        public DeleteUserCommand(Guid id) => Id = id;

        public Guid Id { get; set; }        

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                //todo Criar resource
                .IsNotNull(Id, "CreateUserCommand.Id", "Id inválido"));                
        }
    }
}