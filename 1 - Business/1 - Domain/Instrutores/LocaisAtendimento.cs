using Notification;

namespace Domain.Instrutores
{
    public class LocaisAtendimento
    {
        public int IndInstrutor { get; private set; }
        public int? IdLocalAtendimento { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public LocaisAtendimento(int idInstrutor, string estado, string cidade, string bairro)
        {
            IndInstrutor = idInstrutor;
           // IdLocalAtendimento = idLocalAtendimento;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;

            LocaisValidator();
        }

        private void LocaisValidator()
        {
            Estado.IsNullEmptyOrWhiteSpace("Estado não informado");
            Cidade.IsNullEmptyOrWhiteSpace("Cidade não informada");
            Bairro.IsNullEmptyOrWhiteSpace("Bairro não informado");
            
        }

    }
}
