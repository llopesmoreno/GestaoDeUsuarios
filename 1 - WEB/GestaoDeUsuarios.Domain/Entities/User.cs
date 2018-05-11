using Flunt.Validations;
using GestaoDeUsuarios.Domain.Base;
using GestaoDeUsuarios.Shared.Resources;
using GestaoDeUsuarios.Domain.Base.ValueObjects;

namespace GestaoDeUsuarios.Domain.Entities
{
    public class User : Entitie
    {
        public User(Name name, CPF cPF, string telefone) => Fill(name, cPF, telefone);

        public void Update(Name name, CPF cPF, string telefone) => Fill(name, cPF, telefone);

        public string Value { get; private set; }

        private void Fill(Name name, CPF cPF, string telefone)
        {
            Name = name;
            CPF = cPF;
            Telefone = telefone;
            Validate();
        }

        private void Validate()
        {
            AddNotifications(Name, CPF);
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Telefone, "User.Telefone", Message.TelefoneInvalido));
        }

        public Name Name { get; private set; }
        public CPF CPF { get; private set; }
        public string Telefone { get; private set; }
    }
}