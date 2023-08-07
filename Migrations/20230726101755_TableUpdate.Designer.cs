﻿// <auto-generated />
using System;
using CRUD_Operations_PostGresSQl.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRUD_Operations_PostGresSQl.Migrations
{
    [DbContext(typeof(CrudDbContext))]
    [Migration("20230726101755_TableUpdate")]
    partial class TableUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("CRUD_Operations_PostGresSQl.Models.Domain.Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("TodoName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TodoStatus")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("CRUD_Operations_PostGresSQl.Models.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TodoId")
                        .HasColumnType("uuid");

                    b.Property<bool>("premium")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("TodoId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CRUD_Operations_PostGresSQl.Models.Domain.User", b =>
                {
                    b.HasOne("CRUD_Operations_PostGresSQl.Models.Domain.Todo", "Todo")
                        .WithMany()
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Todo");
                });
#pragma warning restore 612, 618
        }
    }
}
