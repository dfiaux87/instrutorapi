using Data.Connections;
using Domain.Instrutores;
using Domain.Instrutores.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.LocaisAtendimento
{
    public class GerenciaLocaisAtendimentoRepository: ConnectionBase, IGerenciaLocaisAtendimentoRepository
    {
        public GerenciaLocaisAtendimentoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task GravarLocaisAtendimentoAsync(Instrutor instrutores, int idInstrutor)
        {
            // Implementar lógica para gravar os locais de atendimento no banco de dados
        }
    }
}
