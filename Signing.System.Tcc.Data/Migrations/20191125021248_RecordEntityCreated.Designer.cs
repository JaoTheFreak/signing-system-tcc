﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Signing.System.Tcc.Data.Context;

namespace Signing.System.Tcc.Data.Migrations
{
    [DbContext(typeof(SigningContext))]
    [Migration("20191125021248_RecordEntityCreated")]
    partial class RecordEntityCreated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("Relational:Sequence:.User", "'User', '', '12', '1', '', '', 'Int32', 'False'");

            modelBuilder.Entity("Signing.System.Tcc.Domain.RecordAggregate.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("MidiaDescription");

                    b.Property<string>("MidiaExtension");

                    b.Property<string>("MidiaHash");

                    b.Property<string>("MidiaName");

                    b.Property<string>("MidiaResolution");

                    b.Property<int>("MidiaSizeBytes");

                    b.Property<string>("MidiaUrl");

                    b.Property<decimal>("TransactionFee")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("TransactionHash");

                    b.Property<DateTime?>("UpdatedAte");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Record");
                });

            modelBuilder.Entity("Signing.System.Tcc.Domain.UserAggregate.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("nextval('\"User\"')")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("DocumentNumber");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Salt");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Signing.System.Tcc.Domain.RecordAggregate.Record", b =>
                {
                    b.HasOne("Signing.System.Tcc.Domain.UserAggregate.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
