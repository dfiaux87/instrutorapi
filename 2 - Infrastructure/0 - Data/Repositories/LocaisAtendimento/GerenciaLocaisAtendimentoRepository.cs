using Dapper;
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
            try
            {
                var param = new DynamicParameters();
                param.Add("InstrutorId", idInstrutor, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                param.Add("Bairro", instrutores.LocaisAtendimento.Bairro, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("Cidade", instrutores.LocaisAtendimento.Cidade, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("Estado", instrutores.LocaisAtendimento.Estado, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                var sql = @"
                            INSERT INTO LocalAtendimento (IdInstrutor, BAIRRO, CIDADE, ESTADO)
                            VALUES (@InstrutorId, @Bairro, @Cidade, @Estado);
                         ";

                using (var connection = Connection)
                {
                    await connection.ExecuteAsync(sql, param);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
