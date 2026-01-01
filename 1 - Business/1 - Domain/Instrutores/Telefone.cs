using Notification;

namespace Domain.Instrutores
{
    public class Telefone
    {
        public int IdInstrutor { get; private set; }
        public int? IdTelefone { get; private set; }
        public string DDD { get; private set; } 
        public string NumeroTelefone { get; private set; }
        public string TipoTelefone { get; private set; }
        public Telefone(string ddd,string numero, string tipoTelefone, int idInstrutor)
        {
            NumeroTelefone = numero;
            DDD = ddd;
            IdInstrutor = idInstrutor;
//            IdTelefone = idTelefone;
            TipoTelefone = tipoTelefone;

            TelefoneValidator();
        }

        private void TelefoneValidator()
        {
            // Implementar validações
            NumeroTelefone.IsNullEmptyOrWhiteSpace("Número de telefone é obrigatório.");
            DDD.IsNullEmptyOrWhiteSpace("DDD é obrigatório.").MaxLength(2, "DDD deve possuir apenas 2 caracteres");
            IdInstrutor.ToString().IsNullEmptyOrWhiteSpace("Id do instrutor é obrigatório.");
            TipoTelefone.IsNullEmptyOrWhiteSpace("Tipo do telefone é obrigatório.").MaxLength(1,"TipoTelefone deve possuir apenas 1 caractere");
        }
    }
}
