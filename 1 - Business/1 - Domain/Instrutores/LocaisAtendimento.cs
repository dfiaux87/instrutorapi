using Notification;

namespace Domain.Instrutores
{
    public class LocaisAtendimento: Notifiable
    {
        public int? IndInstrutor { get; private set; }
        public int? IdLocalAtendimento { get; private set; }
        public string UF { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public LocaisAtendimento(string uf, string estado, string cidade, string bairro, int? idInstrutor = null)
        {
            UF = uf;
            IndInstrutor = idInstrutor;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;

            LocaisValidator();
        }

        private void LocaisValidator()
        {
            UF.IsNullEmptyOrWhiteSpace("UF não informada")
                .MaxLength(2, "UF deve possuir 2 caracteres");
            Estado.IsNullEmptyOrWhiteSpace("Estado não informado");
            Cidade.IsNullEmptyOrWhiteSpace("Cidade não informada");
            Bairro.IsNullEmptyOrWhiteSpace("Bairro não informado");
            
        }

    }
}
