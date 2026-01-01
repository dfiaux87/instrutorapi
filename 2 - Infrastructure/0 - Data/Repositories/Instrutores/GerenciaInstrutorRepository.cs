using Application.Instrutores.ViewModels;
using Dapper;
using Data.Connections;
using Domain.Instrutores;
using Domain.Instrutores.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Data.Repositories.Instrutores
{
    public class GerenciaInstrutorRepository : ConnectionBase, IGerenciaInstrutorRepository
    {
        public GerenciaInstrutorRepository(IConfiguration configuration) : base(configuration)
        {
        }

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
                throw;
            }

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
    }
}
