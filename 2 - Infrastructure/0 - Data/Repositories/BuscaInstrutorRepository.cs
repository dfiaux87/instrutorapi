using Application.Instrutores.Interfaces;
using Application.Instrutores.ViewModels;
using Data.Connections;
using Domain.Instrutores;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BuscaInstrutorRepository: ConnectionBase, IBuscaInstrutorRepository
    {
        public BuscaInstrutorRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<InstrutorViewModel>> ObterInstrutorPorIdAsync(string id)
        {
            // Implementar lógica para obter um instrutor por ID do banco de dados

            return null;
        }
    }
}
