
using eAgendaMedica.Aplicacao.ModuloAtividade;
using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Infra.Orm;
using eAgendaMedica.Infra.Orm.ModuloAtividade;
using eAgendaMedica.Infra.Orm.ModuloMedico;
using eAgendaMedica.WebAPI.Config.AutoMapperConfig;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.WebApi.Config
{
    public static class InjecaoDependenciaConfigExtension
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSql");

            services.AddDbContext<IContextoPersistencia, eAgendaMedicaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(connectionString);
                
            });

            services.AddTransient<IRepositorioMedico, RepositorioMedicoOrm>();
            services.AddTransient<ServicoMedico>();

            services.AddTransient<IRepositorioAtividade, RepositorioAtividadeOrm>();
            services.AddTransient<ServicoAtividade>();

            services.AddTransient<InserirAtividadeMappingAction>();


            services.AddTransient<InserirTipoDeAtividadeMappingAction>();

        }
    }
}