namespace AgendaApp.API.Configurations
{
    /// <summary>
    /// Classe para configurar as permissões de CORS do projeto
    /// </summary>
    public class CorsConfiguration
    {
        //nome da política de CORS
        public static string PolicyName => "DefaultPolicy";

        /// <summary>
        /// Método para realizar a configuração do CORS no projeto
        /// </summary>
        public static void Configure(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName,
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
    }

}



