using System;
using Flunt.Validations;
using Flunt.Notifications;
using GestaoDeUsuarios.Shared.Resources;
using GestaoDeUsuarios.Domain.Base.Commands;
using static GestaoDeUsuarios.Shared.Base.FuncoesValidacao;

namespace GestaoDeUsuarios.Domain.Commands
{
    public class UpdateUserCommand : Notifiable, ICommand
    {
        public UpdateUserCommand(Guid id, string nome, string sobrenome, string cPF, string telefone)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Telefone = telefone;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                //todo criar resource
                .IsNotNull(Id, "CreateUserCommand.Id", "Id inválido")
                .IsNotNullOrEmpty(Nome, "CreateUserCommand.Nome", Message.NomeInvalido)
                .IsNotNullOrEmpty(Sobrenome, "CreateUserCommand.Sobrenome", Message.SobrenomeInvalido)
                .IsTrue(IsCPFValido(CPF), "CPF.Valor", Message.CPFInvalido)
                .IsNotNullOrEmpty(Telefone, "User.Telefone", Message.TelefoneInvalido));
        }
    }
}