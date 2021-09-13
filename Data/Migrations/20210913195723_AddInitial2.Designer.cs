﻿// <auto-generated />
using System;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(FabricantesContext))]
    [Migration("20210913195723_AddInitial2")]
    partial class AddInitial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Model.Models.FabricanteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataFundacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fundador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeFabricante")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaisOrigem")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Fabricantes");
                });

            modelBuilder.Entity("Domain.Model.Models.ProcessadorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BaseFrequency")
                        .HasColumnType("real");

                    b.Property<int>("Cores")
                        .HasColumnType("int");

                    b.Property<int>("FabricanteModelId")
                        .HasColumnType("int");

                    b.Property<string>("ItemDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LaunchDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeProcessador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Threads")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FabricanteModelId");

                    b.ToTable("Processadores");
                });

            modelBuilder.Entity("Domain.Model.Models.ProcessadorModel", b =>
                {
                    b.HasOne("Domain.Model.Models.FabricanteModel", "Fabricante")
                        .WithMany("Processadores")
                        .HasForeignKey("FabricanteModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fabricante");
                });

            modelBuilder.Entity("Domain.Model.Models.FabricanteModel", b =>
                {
                    b.Navigation("Processadores");
                });
#pragma warning restore 612, 618
        }
    }
}
