using Flunt.Notifications;
using Flunt.Validations;
using GestaoDeUsuarios.Shared.Resources;

namespace GestaoDeUsuarios.Domain.Base.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
            => Fill(firstName, lastName);

        public void Change(string firstName, string lastName)
            => Fill(firstName, lastName);

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private void Fill(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Validate();
        }

        private void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(FirstName, "Name.FirstName", Message.NomeInvalido)
                .IsNotNullOrEmpty(LastName, "Name.LastName", Message.SobrenomeInvalido)
            );
        }
    }
}