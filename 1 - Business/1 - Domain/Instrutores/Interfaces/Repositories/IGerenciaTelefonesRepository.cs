namespace Domain.Instrutores.Interfaces.Repositories
{
    public interface IGerenciaTelefonesRepository
    {
        Task GravarTelefonesAsync(Instrutor instrutor, int idInstrutor);
        Task AtualizarTelefonesAsync(Instrutor instrutor, int idTelefone, int idInstrutor);
        Task RemoverTelefonesAsync(int idInstrutor, int idTelefone);
    }
}
