using Domain.Instrutores.Interfaces.Repositories;
using Domain.Instrutores.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Notification;
using Serilog;
using System.Transactions;

namespace Domain.Instrutores.Services
{
    public class GerenciaInstrutorService : Notifiable, IGerenciaInstrutorService
    {
        private readonly IGerenciaInstrutorRepository _gerenciaInstrutorRepository;
        private readonly IGerenciaLocaisAtendimentoRepository _locaisAtendimentoRepository;
        private readonly IGerenciaTelefonesRepository _gerenciaTelefonesRepository;
        private readonly IConfiguration _configuration;

        public GerenciaInstrutorService(IGerenciaInstrutorRepository gerenciaInstrutorRepository, IConfiguration configuration,
            IGerenciaLocaisAtendimentoRepository locaisAtendimentoRepository, IGerenciaTelefonesRepository gerenciaTelefonesRepository)
        {
            _gerenciaInstrutorRepository = gerenciaInstrutorRepository;
            _locaisAtendimentoRepository = locaisAtendimentoRepository;
            _gerenciaTelefonesRepository = gerenciaTelefonesRepository;
            _configuration = configuration;
        }

        public async Task Gravar(Instrutor instrutor)
        {
            if (instrutor == null)
            {
                AddNotification("Instrutor", "Instrutor não pode ser nulo.");
                return;
            }

            int timeoutValue;
            if (!int.TryParse(_configuration.GetSection("TransactionTimeout").Value, out timeoutValue))
            {
                Log.Error($"Variavel 'TransactionTimeout' nao eh conversivel em inteiro, valor padrao(1 min) definido.");
                timeoutValue = 1;
            }
            else
            {
                Log.Information($"Variavel 'TransactionTimeout' definido em {timeoutValue} minutos.");
            }

            var options = new TransactionOptions
            {
                Timeout = TimeSpan.FromMinutes(timeoutValue)
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, options, asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (instrutor.Id == null)
                    {
                        var idInstrutor = await _gerenciaInstrutorRepository.GravarInstrutorAsync(instrutor);
                        await _locaisAtendimentoRepository.GravarLocaisAtendimentoAsync(instrutor, idInstrutor);
                        await _gerenciaTelefonesRepository.GravarTelefonesAsync(instrutor, idInstrutor);
                    }
                    else
                    {
                        await _gerenciaInstrutorRepository.AtualizarInstrutorAsync(instrutor);
                    }

                }
                catch (Exception ex)
                {
                    AddNotification(ex?.Message, "", NotificationType.Error, "", System.Net.HttpStatusCode.InternalServerError);
                    Log.Error(ex?.Message, ex);
                }
            }


        }

    }
}
