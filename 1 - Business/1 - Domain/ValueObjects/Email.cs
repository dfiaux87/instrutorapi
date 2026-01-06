using Notification;

namespace Domain.ValueObjects
{
    public class Email: Notifiable
    {
        public string Endereco { get; private set; }
        public Email(string endereco)
        {
            Endereco = endereco;
            EmailValidator();
        }
        private void EmailValidator()
        {
            Endereco.IsNullEmptyOrWhiteSpace("Endereço de email é obrigatório.");
            Endereco.MaxLength(100, "Endereço de email deve possuir até 100 caracteres.");
            //Endereco.IsEmail("Endereço de email inválido.");
        }
    }
}
