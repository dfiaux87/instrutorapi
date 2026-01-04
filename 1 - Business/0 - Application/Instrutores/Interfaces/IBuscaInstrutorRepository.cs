using Application.Instrutores.ViewModels;

namespace Application.Instrutores.Interfaces
{
    public interface IBuscaInstrutorRepository
    {
        Task<IEnumerable<InstrutorViewModel>> ObterInstrutorPorIdAsync(string id);
        Task<bool> ObterInstrutorCpfEmail(string cpf, string email);
    }
}
