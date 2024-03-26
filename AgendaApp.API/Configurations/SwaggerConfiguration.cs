using Microsoft.OpenApi.Models;

namespace AgendaApp.API.Configurations
{
    /// <summary>
    /// Classe para configurar a documentação gerada pelo Swagger na API
    /// </summary>
    public class SwaggerConfiguration
    {
        /// <summary>
        /// Método para configuração do swagger
        /// </summary>
        public static void Configure(IServiceCollection services)
        {
            //habilitando o swagger no projeto API
            services.AddEndpointsApiExplorer();

            //detalhando a documentação do swagger
            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AgendaApp - API para agenda de tarefas",
                Description = "API REST .NET com EntityFramework, XUnit, DDD e TDD.",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "COTI Informática",
                    Email = "contato@cotiinformatica.com.br",
                    Url = new Uri("http://www.cotiinformatica.com.br")
                }
            }));
        }
    }

}


