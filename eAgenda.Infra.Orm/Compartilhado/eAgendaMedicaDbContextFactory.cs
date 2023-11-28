using eAgendaMedica.Infra.Orm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GeradorTestes.Infra.Orm
{
    public class eAgendaMedicaDbContextFactory : IDesignTimeDbContextFactory<eAgendaMedicaDbContext>
    {
        public eAgendaMedicaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<eAgendaMedicaDbContext>();

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("PostgreSql");

            builder.UseNpgsql(connectionString);

            return new eAgendaMedicaDbContext(builder.Options);
        }
    }
}
