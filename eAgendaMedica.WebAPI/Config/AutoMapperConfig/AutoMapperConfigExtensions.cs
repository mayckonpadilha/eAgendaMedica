using AutoMapper;
using eAgenda.WebApi.Config.AutoMapperConfig;
using eAgendaMedica.WebAPI.Config.AutoMapperConfig;

namespace eAgendaMedica.WebApi.Config.AutomapperConfig
{
    public static class AutoMapperConfigExtension
    {
        public static void ConfigurarAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.AddProfile<MedicoProfile>();

                mapperConfig.AddProfile<AtividadeProfile>();

                mapperConfig.AddProfile<HorasOcupadasProfile>();
            });
        }
    }
}