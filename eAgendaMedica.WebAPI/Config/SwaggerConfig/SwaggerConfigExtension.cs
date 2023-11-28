using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace eAgenda.WebApi.Config.SwaggerConfig
{
    public static class SwaggerConfigExtension
    {
       public static void ConfigurarSwagger(this IServiceCollection services)
       {
           services.AddEndpointsApiExplorer();

           services.AddSwaggerGen();
       }
        

    }
}
