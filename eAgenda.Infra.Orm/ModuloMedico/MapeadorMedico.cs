using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Infra.Orm.ModuloMedico
{
    public class MapeadorMedico : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("TBMedico");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.CRM).HasColumnType("char(8)").IsRequired();
            builder.Property(x => x.EmAtividade).HasColumnType("bool");

            builder.Ignore(x => x.HorasDeDescanso);

            builder.HasMany(x => x.HorasOcupadas)
                   .WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
