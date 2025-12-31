using Notification;

namespace Domain.Instrutores
{
    public class Instrutor
    {
        public Guid? Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public Telefone Telefone { get; private set; }

        public LocaisAtendimento LocaisAtendimento { get; private set; }
        public Instrutor(string nome, string cpf, string email, Telefone telefone, LocaisAtendimento locaisAtendimento, Guid? id = null)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            LocaisAtendimento = locaisAtendimento;
            Cpf = cpf;

            if (Id == null || Id == Guid.Empty)
                Id = Guid.NewGuid();

            InstrutorValidator();
        }

        private void InstrutorValidator()
        {
            // Implementar validações
            Nome.IsNullEmptyOrWhiteSpace("Nome do instrutor é obrigatório.");
            Email.IsNullEmptyOrWhiteSpace("Email do instrutor é obrigatório.");
            Cpf.IsNullEmptyOrWhiteSpace("CPF do instrutor é obrigatório.")
                .MaxLength(11,"CPF deve possuir até 11 dígitos")
                .ValidCpf("CPF inválido",true);
        }
    }
}
