using Application.Instrutores;
using Application.Instrutores.Interfaces;
using Data.Repositories.Instrutores;
using Data.Repositories.LocaisAtendimento;
using Data.Repositories.Telefones;
using Domain.Instrutores.Interfaces.Repositories;
using Domain.Instrutores.Interfaces.Services;
using Domain.Instrutores.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ioc
{
    public static class DependencyResolver
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            // Registrar repositórios
            services.AddScoped<IGerenciaInstrutorRepository, GerenciaInstrutorRepository>();
            services.AddScoped<IGerenciaLocaisAtendimentoRepository, GerenciaLocaisAtendimentoRepository>();
            services.AddScoped<IGerenciaTelefonesRepository, GerenciaTelefonesRepository>();
            services.AddScoped<IBuscaInstrutorRepository, BuscaInstrutorRepository>();
            
            // Registrar serviços de domínio
            services.AddScoped<IGerenciaInstrutorService, GerenciaInstrutorService>();
            

            // Registrar aplicações
            services.AddScoped<IInstrutoresApplication, InstrutoresApplication>();
             

            return services;
        }
    }
}
