using Domain.Instrutores;

namespace Domain.Instrutores.Interfaces.Repositories
{
    public interface IGerenciaInstrutorRepository
    {
        Task AdicionarInstrutorAsync(Instrutor instrutor);
        Task AtualizarInstrutorAsync(Instrutor instrutor);
        Task RemoverInstrutorAsync(Instrutor instrutor);
    }
}

