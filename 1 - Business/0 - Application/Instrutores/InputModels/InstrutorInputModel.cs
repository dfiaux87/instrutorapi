using Domain.Instrutores;

namespace Application.Instrutores.InputModels
{
    public class InstrutorInputModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public TelefoneInputModel Telefone { get; set; }
        public LocaisAtendimentoInputModel LocaisAtendimento { get; set; }
    }
}
