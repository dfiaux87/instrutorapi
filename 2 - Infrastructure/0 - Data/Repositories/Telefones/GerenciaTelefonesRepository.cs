using Dapper;
using Data.Connections;
using Domain.Instrutores;
using Domain.Instrutores.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Telefones
{
    public class GerenciaTelefonesRepository: ConnectionBase, IGerenciaTelefonesRepository
    {
        public GerenciaTelefonesRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task GravarTelefonesAsync(Instrutor instrutor, int idInstrutor)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("idInstrutor", idInstrutor, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                param.Add("ddd", instrutor.Telefone.DDD, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("numeroTelefone", instrutor.Telefone.NumeroTelefone, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("tipoTelefone", instrutor.Telefone.TipoTelefone, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                var query = @"
                        INSERT INTO TELEFONE (IdInstrutor, DDD, NumeroTelefone, TipoTelefone)
                        VALUES (@idInstrutor, @ddd, @numeroTelefone, @tipoTelefone);
                     ";
                using (var connection = Connection)
                {
                    await connection.ExecuteAsync(query, param);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AtualizarTelefonesAsync(Instrutor instrutor,int idTelefone, int idInstrutor)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("IdTelefone", idTelefone, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                param.Add("IdInstrutor", idInstrutor, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                param.Add("DDD", instrutor.Telefone.DDD, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("NumeroTelefone", instrutor.Telefone.NumeroTelefone, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                param.Add("TipoTelefone", instrutor.Telefone.TipoTelefone, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                var query = @" 
                      update Telefone 
                         set DDD = @DDD,
                             NUMEROTELEFONE = @NumeroTelefone,
                             TIPOTELEFONE = @TipoTelefone
                       where IdTelefone = @IdTelefone 
                         and IdInstrutor = @IdInstrutor;
               ";

                using (var connection = Connection)
                {
                    await connection.ExecuteAsync(query, param);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task RemoverTelefonesAsync(int idInstrutor, int idTelefone)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("IdInstrutor", idInstrutor, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                param.Add("IdTelefone", idTelefone, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);

                var query = @"
                        DELETE FROM TELEFONE WHERE IdInstrutor = @IdInstrutor and IdTelefone = @IdTelefone;
                     ";

                using (var connection = Connection)
                {
                    await connection.ExecuteAsync(query, param);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
