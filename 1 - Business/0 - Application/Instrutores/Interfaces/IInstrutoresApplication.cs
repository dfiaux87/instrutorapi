using Application.Instrutores.InputModels;
using Application.Instrutores.ViewModels;


namespace Application.Instrutores.Interfaces
{
    public interface IInstrutoresApplication
    {
        Task<IEnumerable<InstrutorViewModel>> ObterInstrutorPorIdAsync(string id);
        Task AdicionarInstrutorAsync(InstrutorInputModel instrutor);
    }
}
