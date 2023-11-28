﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using eAgendaMedica.Infra.Orm;

#nullable disable

namespace eAgendaMedica.Infra.Orm.Migrations
{
    [DbContext(typeof(eAgendaMedicaDbContext))]
    partial class eAgendaMedicaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AtividadeMedico", b =>
                {
                    b.Property<Guid>("AtividadesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MedicosId")
                        .HasColumnType("uuid");

                    b.HasKey("AtividadesId", "MedicosId");

                    b.HasIndex("MedicosId");

                    b.ToTable("TBAtividade_TBMedico", (string)null);
                });

            modelBuilder.Entity("eAgendaMedica.Dominio.ModuloAtividade.Atividade", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Assunto")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("DataRealizacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Finalizada")
                        .HasColumnType("bool");

                    b.Property<long>("HoraInicio")
                        .HasColumnType("bigint");

                    b.Property<long>("HoraTermino")
                        .HasColumnType("bigint");

                    b.Property<long>("TempoDeDescanso")
                        .HasColumnType("bigint");

                    b.Property<int>("TipoAtividadeEnum")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TBAtividade", (string)null);
                });

            modelBuilder.Entity("eAgendaMedica.Dominio.ModuloMedico.HoraOcupada", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DiaDaAtividade")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("HoraFinal")
                        .HasColumnType("bigint");

                    b.Property<long>("HoraInicio")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("MedicoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId");

                    b.ToTable("TBHoraOcupadas", (string)null);
                });

            modelBuilder.Entity("eAgendaMedica.Dominio.ModuloMedico.Medico", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("char(8)");

                    b.Property<bool>("EmAtividade")
                        .HasColumnType("bool");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TBMedico", (string)null);
                });

            modelBuilder.Entity("AtividadeMedico", b =>
                {
                    b.HasOne("eAgendaMedica.Dominio.ModuloAtividade.Atividade", null)
                        .WithMany()
                        .HasForeignKey("AtividadesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAgendaMedica.Dominio.ModuloMedico.Medico", null)
                        .WithMany()
                        .HasForeignKey("MedicosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eAgendaMedica.Dominio.ModuloMedico.HoraOcupada", b =>
                {
                    b.HasOne("eAgendaMedica.Dominio.ModuloMedico.Medico", null)
                        .WithMany("HorasOcupadas")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eAgendaMedica.Dominio.ModuloMedico.Medico", b =>
                {
                    b.Navigation("HorasOcupadas");
                });
#pragma warning restore 612, 618
        }
    }
}