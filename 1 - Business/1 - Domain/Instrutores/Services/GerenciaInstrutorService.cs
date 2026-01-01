using Domain.Instrutores.Interfaces.Repositories;
using Domain.Instrutores.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notification;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Domain.Instrutores.Services
{
    public class GerenciaInstrutorService: Notifiable, IGerenciaInstrutorService
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
                Timeout = TimeSpan.FromMinutes(timeoutValue) // Define o timeout para 5 minutos
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, options, asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var idInstrutor = await _gerenciaInstrutorRepository.GravarInstrutorAsync(instrutor);
                    await _locaisAtendimentoRepository.GravarLocaisAtendimentoAsync(instrutor, idInstrutor);
                    await _gerenciaTelefonesRepository.GravarTelefonesAsync(instrutor, idInstrutor);
                     
                }
                catch (Exception ex)
                {
                    AddNotification(ex?.Message, "", NotificationType.Error, "", System.Net.HttpStatusCode.InternalServerError);
                    Log.Error(ex?.Message, ex);
                }
            }
            if (instrutor.Id == null)
            {
                await _gerenciaInstrutorRepository.AdicionarInstrutorAsync(instrutor);
            }
            else
            {
                await _gerenciaInstrutorRepository.AtualizarInstrutorAsync(instrutor);
            }
        }

    }
}
