using Notification;

namespace Domain.Instrutores
{
    public class LocaisAtendimento
    {
        public Guid IndInstrutor { get; private set; }
        public Guid? IdLocalAtendimento { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public LocaisAtendimento(Guid idInstrutor, string estado, string cidade, string bairro, Guid? idLocalAtendimento = null)
        {
            IndInstrutor = idInstrutor;
            IdLocalAtendimento = idLocalAtendimento;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;

            if (idLocalAtendimento == null)
                IdLocalAtendimento = Guid.NewGuid();

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
