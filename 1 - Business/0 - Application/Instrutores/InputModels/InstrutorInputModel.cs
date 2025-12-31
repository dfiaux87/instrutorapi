using Domain.Instrutores;

namespace Application.Instrutores.InputModels
{
    public class InstrutorInputModel
    {
        public string Nome { get; set; }
        public string? Email { get; set; }
        public Telefone? Telefone { get; set; }
        public LocaisAtendimento? LocaisAtendimento { get; set; }
    }
}
