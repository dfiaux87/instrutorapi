using Application.Instrutores.ViewModels;
using Dapper;
using Data.Connections;
using Domain.Instrutores;
using Domain.Instrutores.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Data.Repositories.Instrutores
{
    public class GerenciaInstrutorRepository : ConnectionBase, IGerenciaInstrutorRepository
    {
        public GerenciaInstrutorRepository(IConfiguration configuration, ILogger<GerenciaInstrutorRepository> logger) 
            : base(configuration)
        {
            _logger = logger;
        }

        private ILogger<GerenciaInstrutorRepository> _logger;

        public async Task<int> GravarInstrutorAsync(Instrutor instrutor)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Nome", instrutor.Nome, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("Cpf", instrutor.Cpf, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("Email", instrutor.Email, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                var query = @"
                            INSERT INTO INSTRUTORES (NOME, CPF, EMAIL)
                            VALUES (@NOME, @CPF, @EMAIL);
                            
                            SELECT CAST(SCOPE_IDENTITY() as int);
                         ";
                using (var connection = Connection)
                {
                    return await connection.QueryFirstOrDefaultAsync<int>(query, param);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao adicionar instrutor: {Mensagem}", ex.Message);
                throw;
            }

        }
        public async Task AtualizarInstrutorAsync(Instrutor instrutor)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Nome", instrutor.Nome, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("Cpf", instrutor.Cpf, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("Email", instrutor.Email, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                var query = @"
                            UPDATE INSTRUTORES
                            SET NOME = @NOME,
                                CPF = @CPF,
                                EMAIL = @EMAIL
                            WHERE ID = @ID;
                         ";
                using (var connection = Connection)
                {
                    await connection.ExecuteAsync(query, param);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao atualizar instrutor: {Mensagem}", ex.Message);
                throw;
            }
        }
        public async Task RemoverInstrutorAsync(Instrutor instrutor)
        {
            try
            {
                if (instrutor == null || instrutor.Id == null)
                   throw new ArgumentNullException(nameof(instrutor), "Instrutor ou Id não pode ser nulo.");

                var param = new DynamicParameters();
                param.Add("Id", instrutor.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

            var query = @"
                              
                               DELETE FROM LOCAISATENDIMENTO WHERE INSTRUTORID = @ID;
                               
                               DELETE FROM TELEFONE WHERE INSTRUTORID = @ID;

                               DELETE FROM INSTRUTORES WHERE ID = @ID;
                     
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
            catch (Exception ex)
            {
                _logger.LogError("Erro ao remover instrutor: {Mensagem}", ex.Message);
                throw;
            }
        }
    }
}
