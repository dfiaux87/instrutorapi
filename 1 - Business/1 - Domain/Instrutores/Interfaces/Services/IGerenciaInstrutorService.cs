namespace Domain.Instrutores.Interfaces.Services
{
    public interface IGerenciaInstrutorService
    {
        Task Gravar(Instrutor instrutor);
        Task AtualizarInstrutorAsync(Instrutor instrutor);
        Task RemoverInstrutorAsync(string id);
    }
}
