using Application.Instrutores.InputModels;
using Application.Instrutores.Interfaces;
using Application.Instrutores.ViewModels;
using Domain.Instrutores;
using Domain.Instrutores.Interfaces.Repositories;
using Domain.Instrutores.Interfaces.Services;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Notification;
using Serilog;


namespace Application.Instrutores
{
    public class InstrutoresApplication : Notifiable, IInstrutoresApplication
    {
        //private readonly IInstrutoresApplication _instrutoresApplication;
        private readonly IGerenciaInstrutorService _gerenciaInstrutorService;
        private readonly IBuscaInstrutorRepository _buscaInstrutorRepository;
        private readonly IGerenciaLocaisAtendimentoRepository _gerenciaLocaisAtendimentoRepository;
        private readonly IGerenciaTelefonesRepository _gerenciaTelefonesRepository;
        private ILogger<InstrutoresApplication> _logger;

        public InstrutoresApplication(IGerenciaInstrutorService gerenciaInstrutorService,
            IGerenciaTelefonesRepository gerenciaTelefonesRepository,
            IGerenciaLocaisAtendimentoRepository gerenciaLocaisAtendimentoRepository,
            ILogger<InstrutoresApplication> logger)
        {
            _gerenciaInstrutorService = gerenciaInstrutorService;
            _gerenciaTelefonesRepository = gerenciaTelefonesRepository;
            _gerenciaLocaisAtendimentoRepository = gerenciaLocaisAtendimentoRepository;
            _logger = logger;
        }

        public async Task AdicionarInstrutorAsync(InstrutorInputModel instrutor)
        {
            try
            {
                if(await ObterInstrutorCpfEmail(instrutor.Cpf, instrutor.Email))
                {
                    _logger.LogWarning($"Tentativa de adicionar instrutor com CPF ou Email já existente: {instrutor.Cpf}, {instrutor.Email}");
                    AddNotification("Duplicado", "Já existe um instrutor cadastrado com este CPF ou Email.");
                    return;
                }

                _logger.LogInformation("Validando domínios (telefone, locais de atendimento e instrutor");
                var _telefone = new Telefone(instrutor.Telefone.DDD, instrutor.Telefone.NumeroTelefone, instrutor.Telefone.TipoTelefone);

                var _locaisAtendimento = new LocaisAtendimento(instrutor.LocaisAtendimento.UF, instrutor.LocaisAtendimento.Estado, 
                                                               instrutor.LocaisAtendimento.Cidade, instrutor.LocaisAtendimento.Bairro);

                var _email = new Email(instrutor.Email);
                if(_email.HasInvalidNotification)
                    return;
                
                var _documento = new Documento(instrutor.Cpf);
                if(_documento.HasInvalidNotification)
                    return;

                var _instrutor = new Instrutor(instrutor.Nome, _documento, _email, _telefone, _locaisAtendimento);

                if(_telefone.HasInvalidNotification || _locaisAtendimento.HasInvalidNotification || _instrutor.HasInvalidNotification)
                {
                    _logger.LogWarning("Instrutor não válido para prosseguir com o cadastro");
                    return;
                }

                _logger.LogInformation("Validações de domínios (telefone, locais de atendimento e instrutor");
                _logger.LogInformation($"Iniciando adição de instrutor: {instrutor.Nome}");
                await _gerenciaInstrutorService.Gravar(_instrutor);
               

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao adicionar instrutor: {Mensagem}", ex.Message);
                AddNotification("Erro", "Ocorreu um erro ao adicionar o instrutor.");
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
                _logger.LogError("Erro ao atualizar instrutor: {Mensagem}", ex.Message);
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
                _logger.LogError("Erro ao remover instrutor: {Mensagem}", ex.Message);
                this.AddNotification("Erro", "Ocorreu um erro ao remover o instrutor.");
            }
        }

        public async Task<IEnumerable<InstrutorViewModel>> ObterInstrutorPorIdAsync(string id)
        {
            _logger.LogInformation($"Buscando instrutor pelo ID: {id}");
            return await _buscaInstrutorRepository.ObterInstrutorPorIdAsync(id);
            
        }

        public async Task<bool> ObterInstrutorCpfEmail (string cpf, string email)
        {
            _logger.LogInformation("Verificando existência de instrutor com CPF: {Cpf} e Email: {Email}", cpf, email);
            return await _buscaInstrutorRepository.ObterInstrutorCpfEmail(cpf, email);
        }
    }
}
