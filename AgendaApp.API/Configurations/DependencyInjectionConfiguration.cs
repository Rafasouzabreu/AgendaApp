using AgendaApp.Domain.Interfaces.Repositories;
using AgendaApp.Domain.Interfaces.Services;
using AgendaApp.Domain.Services;
using AgendaApp.Infra.Data.Repositories;

namespace AgendaApp.API.Configurations
{
    public class DependencyInjectionConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ITarefaDomainService, TarefaDomainService>();
            services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();

            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }
    }
}



