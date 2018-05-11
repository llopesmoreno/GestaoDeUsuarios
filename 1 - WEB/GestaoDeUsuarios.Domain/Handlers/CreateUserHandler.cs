using Flunt.Notifications;
using GestaoDeUsuarios.Domain.Base;
using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Commands;
using GestaoDeUsuarios.Shared.Resources;
using GestaoDeUsuarios.Domain.Repositories;
using GestaoDeUsuarios.Domain.Base.ValueObjects;

namespace GestaoDeUsuarios.Domain.Handlers
{
    public class CreateUserHandler : Notifiable, 
        IHandler<CreateUserCommand>
    {
        public CreateUserHandler(IUserRepository repository) => userRepository = repository;        

        private readonly IUserRepository userRepository;

        public CommandResult Handle(CreateUserCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Dados de cadastro inválidos");
            }

            if (userRepository.CPFExists(new CPF(command.CPF)))
                AddNotification("CPF", "Este CPF já está cadastrado!");

            var nome = new Name(command.Nome, command.Sobrenome);
            var cpf = new CPF(command.CPF);

            var usuario = new User(nome, cpf, command.Telefone);

            if (usuario.Invalid)
            {
                AddNotifications(usuario);
                return new CommandResult(false, "Dados de cadastro inválidos");
            }

            userRepository.Create(usuario);

            return new CommandResult(true, Message.CadastroRealizadoComSucesso);
        }
       
    }
}