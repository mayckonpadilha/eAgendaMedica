using eAgendaMedica.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eAgendaMedica.Infra.Orm
{
    public class eAgendaMedicaDbContext : DbContext, IContextoPersistencia
    {
       
        public eAgendaMedicaDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }    

        public void GravarDados()
        {
            SaveChanges();
        }

        public async Task GravarDadosAsync()
        {
            await SaveChangesAsync();
        }

        public void DesfazerAlteracoes()
        {
            var registrosAlterados = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList();

            foreach (var registro in registrosAlterados)
            {
                switch (registro.State)
                {
                    case EntityState.Added:
                        registro.State = EntityState.Detached;
                        break;

                    case EntityState.Deleted:
                        registro.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        registro.State = EntityState.Unchanged;
                        registro.CurrentValues.SetValues(registro.OriginalValues);
                        break;

                    default:
                        break;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            ILoggerFactory loggerFactory = LoggerFactory.Create((x) =>
            {
                x.AddSerilog(Log.Logger);
            });

            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Type tipo = typeof(eAgendaMedicaDbContext);

            Assembly dllComConfiguracoesOrm = tipo.Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(dllComConfiguracoesOrm);

            base.OnModelCreating(modelBuilder);
        }
    }
}
