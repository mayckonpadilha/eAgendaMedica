using eAgendaMedica.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.Infra.Orm.ModuloMedico
{
    public class MapeadorHorasOcupadas : IEntityTypeConfiguration<HoraOcupada>
    {
        public void Configure(EntityTypeBuilder<HoraOcupada> builder)
        {
            builder.ToTable("TBHoraOcupadas");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.DiaDaAtividade).IsRequired();
            builder.Property(x => x.HoraInicio).IsRequired().HasColumnType("bigint").IsRequired();
            builder.Property(x => x.HoraFinal).IsRequired().HasColumnType("bigint").IsRequired();

        }
    }
}
