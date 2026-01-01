namespace Domain.Instrutores.Interfaces.Repositories
{
    public interface IGerenciaLocaisAtendimentoRepository
    {
        Task GravarLocaisAtendimentoAsync(Instrutor instrutores, int idInstrutor);
    }
}
