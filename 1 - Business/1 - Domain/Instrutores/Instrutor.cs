using Domain.ValueObjects;
using Notification;

namespace Domain.Instrutores
{
    public class Instrutor: Notifiable
    {
        public int? Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public Telefone Telefone { get; private set; }

        public LocaisAtendimento LocaisAtendimento { get; private set; }

        public Instrutor(string nome, string cpf, string email, Telefone telefone, LocaisAtendimento locaisAtendimento)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            LocaisAtendimento = locaisAtendimento;
            Cpf = cpf;

            InstrutorValidator();
        }

        public Instrutor(string nome, string cpf, string email, Telefone telefone, LocaisAtendimento locaisAtendimento, int id)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            LocaisAtendimento = locaisAtendimento;
            Cpf = cpf;

            InstrutorValidator();
        }


        private void InstrutorValidator()
        {
            // Implementar validações
            Nome.IsNullEmptyOrWhiteSpace("Nome do instrutor é obrigatório.")
                .MinLength(3,"Nome deve possuir mais de 3 caracteres")
                .MaxLength(100, "Nome deve possuir até 100 caracteres");

            Cpf.IsNullEmptyOrWhiteSpace("Número do documento é obrigatório.")
              .MaxLength(11, "CPF deve possuir até 11 dígitos")
               .ValidCpf("CPF inválido", true);

            Email.IsNullEmptyOrWhiteSpace("Endereço de email é obrigatório.");
            Email.MaxLength(100, "Endereço de email deve possuir até 100 caracteres.");
            Email.ValidEmail("Endereço de email inválido.");

        }
    }
}
