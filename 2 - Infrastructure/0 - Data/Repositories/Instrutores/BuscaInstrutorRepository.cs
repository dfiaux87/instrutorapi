using Application.Instrutores.Interfaces;
using Application.Instrutores.ViewModels;
using Dapper;
using Data.Connections;
using Domain.Instrutores;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Instrutores
{
    public class BuscaInstrutorRepository: ConnectionBase, IBuscaInstrutorRepository
    {
        public BuscaInstrutorRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<InstrutorViewModel>> ObterInstrutorPorIdAsync(string id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

            var sql = @"
                        SELECT 
                            I.Id, I.Nome, I.Cpf, I.Email,
                            T.TipoTelefone, T.DDD, T.Numero,
                            L.Endereco, L.Cidade, L.Estado
                        FROM INSTRUTORES I
                        LEFT JOIN TELEFONE T ON I.IdInstrutor = T.IdInstrutor
                        LEFT JOIN LOCAISATENDIMENTO L ON I.IdInstrutor = L.IdInstrutor
                        WHERE I.IdInstrutor = @Id
                    ";

            using (var connection = Connection)
            {
                return await connection.QueryAsync<InstrutorViewModel>(sql, param);
                  
            }   
        }

        public async Task<bool> ObterInstrutorCpfEmail(string cpf, string email)
        {
            var param = new DynamicParameters();
            param.Add("Cpf", cpf, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("Email", email, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            var sql = @"
                        SELECT COUNT(1)
                        FROM INSTRUTORES
                        WHERE Cpf = @Cpf OR Email = @Email
                    ";
            using (var connection = Connection)
            {
                var count = await connection.ExecuteScalarAsync<int>(sql, param);
                return count > 0 ? true : false;
            }
        }
    }
}
