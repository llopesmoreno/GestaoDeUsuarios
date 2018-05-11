using Flunt.Validations;
using Flunt.Notifications;
using GestaoDeUsuarios.Shared.Resources;
using GestaoDeUsuarios.Domain.Base.Commands;
using static GestaoDeUsuarios.Shared.Base.FuncoesValidacao;

namespace GestaoDeUsuarios.Domain.Commands
{
    public class CreateUserCommand : Notifiable, ICommand
    {
        public CreateUserCommand(string nome, string sobrenome, string cPF, string telefone)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Telefone = telefone;
        }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Nome, "CreateUserCommand.Nome", Message.NomeInvalido)
                .IsNotNullOrEmpty(Sobrenome, "CreateUserCommand.Sobrenome", Message.SobrenomeInvalido)
                .IsTrue(IsCPFValido(CPF), "CPF.Valor", Message.CPFInvalido)
                .IsNotNullOrEmpty(Telefone, "User.Telefone", Message.TelefoneInvalido));
        }
    }
}
