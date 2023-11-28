using eAgenda.WebApi.Config.SwaggerConfig;
using eAgenda.WebApi.Config;
using eAgenda.WebApi.Filters;
using eAgendaMedica.WebApi.Config.AutomapperConfig;
using eAgendaMedica.WebApi.Config;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace eAgendaMedica.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.
            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;

            });



            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .WriteTo.Console()
              .CreateLogger();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "https://e-agenda-medica-e9bc.onrender.com")
                               .AllowAnyHeader()
                               .AllowAnyMethod();

                    });
            });

            builder.Logging.ClearProviders();

            builder.Services.AddSerilog(Log.Logger);

            builder.Services.ConfigurarInjecaoDependencia(builder.Configuration);

            builder.Services.ConfigurarAutoMapper();

            builder.Services.ConfigurarSwagger();

            builder.Services.AddControllers(config =>
            {
                config.Filters.Add<SerilogActionFilter>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseMiddleware<ManipuladorExcecoes>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}