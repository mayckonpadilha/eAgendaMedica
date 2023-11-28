using eAgendaMedica.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgendaMedica.Dominio.ModuloAtividade;

namespace eAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class MapeadorAtividade : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder)
        {
            builder.ToTable("TBAtividade");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.DataRealizacao).IsRequired();
            builder.Property(x => x.HoraInicio).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.HoraTermino).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.Finalizada).HasColumnType("bool").IsRequired();
            builder.Property(x => x.TipoAtividadeEnum).HasConversion<int>().IsRequired();

            builder.Property(x => x.Assunto).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.TempoDeDescanso).HasColumnType("bigint").IsRequired();

            builder.HasMany(x => x.Medicos)
                .WithMany(x => x.Atividades)
                .UsingEntity(x =>
                    x.ToTable("TBAtividade_TBMedico")
                );
        }
    }
}
