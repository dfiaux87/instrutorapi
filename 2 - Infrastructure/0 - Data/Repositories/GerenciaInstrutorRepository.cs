using Application.Instrutores.ViewModels;
using Dapper;
using Data.Connections;
using Domain.Instrutores;
using Domain.Instrutores.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Data.Repositories
{
    public class GerenciaInstrutorRepository : ConnectionBase, IGerenciaInstrutorRepository
    {
        public GerenciaInstrutorRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task AdicionarInstrutorAsync(Instrutor instrutor)
        {
            // Implementar lógica para adicionar um instrutor ao banco de dados
        }
        public async Task AtualizarInstrutorAsync(Instrutor instrutor)
        {
            // Implementar lógica para atualizar um instrutor no banco de dados
        }
        public async Task RemoverInstrutorAsync(Instrutor instrutor)
        {
            if (instrutor == null || instrutor.Id == null)
               throw new ArgumentNullException(nameof(instrutor), "Instrutor ou Id não pode ser nulo.");

            var param = new DynamicParameters();
            param.Add("Id", instrutor.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

            var query = @"
                              
                               delete from LocaisAtendimento where InstrutorId = @Id;
                               
                               delete from Telefone where InstrutorId = @Id;

                               DELETE FROM Instrutores WHERE Id = @Id;
                     
                              ";

            using (var connection = Connection)
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(query, param, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
