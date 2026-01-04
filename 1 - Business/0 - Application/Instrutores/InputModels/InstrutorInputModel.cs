using Domain.Instrutores;

namespace Application.Instrutores.InputModels
{
    public class InstrutorInputModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public Telefone Telefone { get; set; }
        public LocaisAtendimento LocaisAtendimento { get; set; }
    }
}
