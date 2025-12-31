using Domain.Instrutores;

namespace Application.Instrutores.ViewModels
{
    public class InstrutorViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string? Email { get; set; }
        public string TipoTelefone { get; set; }
        public Telefone? Telefone { get; set; }
        public LocaisAtendimento? LocaisAtendimento { get; set; }

    }
}
