namespace Domain.Instrutores.Interfaces.Repositories
{
    public interface IGerenciaInstrutorRepository
    {
        Task<int> GravarInstrutorAsync(Instrutor instrutor);
        Task AtualizarInstrutorAsync(Instrutor instrutor);
        Task RemoverInstrutorAsync(string id);
         
    }
}

