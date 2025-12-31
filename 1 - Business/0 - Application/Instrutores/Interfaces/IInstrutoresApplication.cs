using Application.Instrutores.ViewModels;

namespace Application.Instrutores.Interfaces
{
    public interface IInstrutoresApplication
    {
        Task<IEnumerable<InstrutorViewModel>> ObterInstrutorPorIdAsync(string id);
    }
}
