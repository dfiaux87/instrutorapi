using Notification;
using Notification.ValueObjects;

namespace Domain.ValueObjects
{
    public class Documento: Notifiable
    {
        public string NumeroDocumento { get; private set; }
        //public string TipoDocumento { get; private set; }
        public Documento(string numeroDocumento)
        {
            NumeroDocumento = numeroDocumento;
            //TipoDocumento = tipoDocumento;
            DocumentosValidator();
        }
        private void DocumentosValidator()
        {
            NumeroDocumento.IsNullEmptyOrWhiteSpace("Número do documento é obrigatório.")
                .MaxLength(11, "CPF deve possuir até 11 dígitos")
                 .ValidCpf("CPF inválido", true); 
            // TipoDocumento.IsNullEmptyOrWhiteSpace("Tipo do documento é obrigatório.");
                
        }
    }
}
