﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orcamentos.Infrastructure;

#nullable disable

namespace Orcamentos.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Orcamentos.Models.BuManager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("buManagers");
                });

            modelBuilder.Entity("Orcamentos.Models.BusinessUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("buManagerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("buManagerId");

                    b.ToTable("businessUnits");
                });

            modelBuilder.Entity("Orcamentos.Models.Orcamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int>("CustoDesc1")
                        .HasColumnType("int");

                    b.Property<int>("CustoDesc2")
                        .HasColumnType("int");

                    b.Property<int>("CustoDesc3")
                        .HasColumnType("int");

                    b.Property<int>("CustoTabela")
                        .HasColumnType("int");

                    b.Property<int>("DescontoTabela")
                        .HasColumnType("int");

                    b.Property<decimal>("MG")
                        .HasPrecision(14, 3)
                        .HasColumnType("decimal(14,3)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Margin")
                        .HasColumnType("int");

                    b.Property<int>("Partnumb")
                        .HasColumnType("int");

                    b.Property<int>("PrecoParcial")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("SerialNumb")
                        .HasColumnType("int");

                    b.Property<string>("TipoUni")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalCost")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<int>("UnitCost")
                        .HasColumnType("int");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.Property<int>("businessUnitId")
                        .HasColumnType("int");

                    b.Property<string>("modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("orcamentoNomeId")
                        .HasColumnType("int");

                    b.Property<int>("profileId")
                        .HasColumnType("int");

                    b.Property<int>("revenueTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("businessUnitId");

                    b.HasIndex("orcamentoNomeId");

                    b.HasIndex("profileId");

                    b.HasIndex("revenueTypeId");

                    b.ToTable("orcamentos");
                });

            modelBuilder.Entity("Orcamentos.Models.OrcamentoNome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("orcamentoNomes");
                });

            modelBuilder.Entity("Orcamentos.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("profileLevelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("profileLevelId");

                    b.ToTable("profiles");
                });

            modelBuilder.Entity("Orcamentos.Models.ProfileLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("profileLevels");
                });

            modelBuilder.Entity("Orcamentos.Models.RevenueType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("revenueTypes");
                });

            modelBuilder.Entity("Orcamentos.Models.BusinessUnit", b =>
                {
                    b.HasOne("Orcamentos.Models.BuManager", "BuManager")
                        .WithMany()
                        .HasForeignKey("buManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuManager");
                });

            modelBuilder.Entity("Orcamentos.Models.Orcamento", b =>
                {
                    b.HasOne("Orcamentos.Models.BusinessUnit", "BusinessUnit")
                        .WithMany()
                        .HasForeignKey("businessUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Orcamentos.Models.OrcamentoNome", "OrcamentoNome")
                        .WithMany()
                        .HasForeignKey("orcamentoNomeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Orcamentos.Models.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("profileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Orcamentos.Models.RevenueType", "RevenueType")
                        .WithMany()
                        .HasForeignKey("revenueTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessUnit");

                    b.Navigation("OrcamentoNome");

                    b.Navigation("Profile");

                    b.Navigation("RevenueType");
                });

            modelBuilder.Entity("Orcamentos.Models.Profile", b =>
                {
                    b.HasOne("Orcamentos.Models.ProfileLevel", "ProfileLevel")
                        .WithMany()
                        .HasForeignKey("profileLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfileLevel");
                });
#pragma warning restore 612, 618
        }
    }
}
