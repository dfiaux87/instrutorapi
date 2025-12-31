using Application.Instrutores.Interfaces;
using Application.Instrutores.ViewModels;
using Domain.Instrutores.Interfaces.Services;
using Notification;
using Serilog;


namespace Application.Instrutores
{
    public class InstrutoresApplication : Notifiable, IInstrutoresApplication
    {
        private readonly IInstrutoresApplication _instrutoresApplication;
        private readonly IGerenciaInstrutorService _gerenciaInstrutorService;
        private readonly IBuscaInstrutorRepository _buscaInstrutorRepository;

        public InstrutoresApplication(IInstrutoresApplication instrutoresApplication, IGerenciaInstrutorService gerenciaInstrutorService)
        {
            _instrutoresApplication = instrutoresApplication;
            _gerenciaInstrutorService = gerenciaInstrutorService;
        }

        public async Task AdicionarInstrutorAsync(/* Parâmetros necessários */)
        {
            try
            {
                // Lógica para adicionar um instrutor
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao adicionar instrutor: {Mensagem}", ex.Message);
                this.AddNotification("Erro", "Ocorreu um erro ao adicionar o instrutor.");
            }
        }

        public async Task AtualizarInstrutorAsync(/* Parâmetros necessários */)
        {
            try
            {
                // Lógica para atualizar um instrutor
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao atualizar instrutor: {Mensagem}", ex.Message);
                this.AddNotification("Erro", "Ocorreu um erro ao atualizar o instrutor.");
            }
        }

        public async Task RemoverInstrutorAsync(/* Parâmetros necessários */)
        {
            try
            {
                // Lógica para remover um instrutor
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao remover instrutor: {Mensagem}", ex.Message);
                this.AddNotification("Erro", "Ocorreu um erro ao remover o instrutor.");
            }
        }

        public async Task<IEnumerable<InstrutorViewModel>> ObterInstrutorPorIdAsync(string id)
        {
            
            return await _buscaInstrutorRepository.ObterInstrutorPorIdAsync(id);
            
        }
    }
}
