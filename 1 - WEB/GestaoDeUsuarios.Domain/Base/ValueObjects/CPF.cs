using Flunt.Validations;
using Flunt.Notifications;
using GestaoDeUsuarios.Shared.Resources;
using static GestaoDeUsuarios.Shared.Base.FuncoesValidacao;

namespace GestaoDeUsuarios.Domain.Base.ValueObjects
{
    public class CPF : Notifiable
    {
        public CPF(string value)
          => Fill(value);

        public void Change(string value)
            => Fill(value);

        public string Value { get; private set; }        

        private void Fill(string value)
        {
            Value = value;
            Validate();
        }

        private void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsTrue(IsCPFValido(Value), "CPF.Valor", Message.CPFInvalido)
            );
        }
    }
}