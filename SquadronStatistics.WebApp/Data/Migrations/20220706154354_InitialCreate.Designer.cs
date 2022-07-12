﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SquadronStatistics.WebApp.Data;

namespace SquadronStatistics.WebApp.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220706154354_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("SquadronStatistics.WebApp.Data.Entities.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smalldatetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("SquadronStatistics.WebApp.Data.Entities.FileRecord", b =>
                {
                    b.Property<int>("RowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FileId")
                        .HasColumnType("int");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("RowId");

                    b.HasIndex("FileId");

                    b.ToTable("FileRecords");
                });

            modelBuilder.Entity("SquadronStatistics.WebApp.Data.Entities.FileRecord", b =>
                {
                    b.HasOne("SquadronStatistics.WebApp.Data.Entities.File", "File")
                        .WithMany("FileRecords")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("SquadronStatistics.WebApp.Data.Entities.File", b =>
                {
                    b.Navigation("FileRecords");
                });
#pragma warning restore 612, 618
        }
    }
}