﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TimeSheets.Data.EntityConfiguration;

namespace TimeSheets.Migrations
{
    [DbContext(typeof(TimesheetDbContext))]
    partial class TimesheetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("TimeSheets.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("User")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("clients");
                });

            modelBuilder.Entity("TimeSheets.Models.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("contracts");
                });

            modelBuilder.Entity("TimeSheets.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("TimeSheets.Models.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("invoices");
                });

            modelBuilder.Entity("TimeSheets.Models.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("services");
                });

            modelBuilder.Entity("TimeSheets.Models.Sheet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InvoiceId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("sheets");
                });

            modelBuilder.Entity("TimeSheets.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("TimeSheets.Models.Invoice", b =>
                {
                    b.HasOne("TimeSheets.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("TimeSheets.Models.Sheet", b =>
                {
                    b.HasOne("TimeSheets.Models.Contract", "Contract")
                        .WithMany("Sheets")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeSheets.Models.Employee", "Employee")
                        .WithMany("Sheets")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeSheets.Models.Invoice", "Invoice")
                        .WithMany("Sheets")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("TimeSheets.Models.Service", "Service")
                        .WithMany("Sheets")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Employee");

                    b.Navigation("Invoice");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("TimeSheets.Models.Contract", b =>
                {
                    b.Navigation("Sheets");
                });

            modelBuilder.Entity("TimeSheets.Models.Employee", b =>
                {
                    b.Navigation("Sheets");
                });

            modelBuilder.Entity("TimeSheets.Models.Invoice", b =>
                {
                    b.Navigation("Sheets");
                });

            modelBuilder.Entity("TimeSheets.Models.Service", b =>
                {
                    b.Navigation("Sheets");
                });
#pragma warning restore 612, 618
        }
    }
}
