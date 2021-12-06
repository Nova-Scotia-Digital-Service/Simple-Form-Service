﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SimpleFormsService.API.Data;

#nullable disable

namespace SimpleFormsService.API.Migrations
{
    [DbContext(typeof(PostgreSqlContext))]
    [Migration("20211129143836_initial-migration")]
    partial class initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SimpleFormsService.API.Entities.Form_Submission", b =>
                {
                    b.Property<Guid>("Submission_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Form_TemplateTemplate_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Json_Data")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<Guid>("Template_Id")
                        .HasColumnType("uuid");

                    b.HasKey("Submission_Id");

                    b.HasIndex("Form_TemplateTemplate_Id");

                    b.ToTable("Form_Submissions");
                });

            modelBuilder.Entity("SimpleFormsService.API.Entities.Form_Template", b =>
                {
                    b.Property<Guid>("Template_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Json_Config")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Template_Id");

                    b.ToTable("Form_Templates");
                });

            modelBuilder.Entity("SimpleFormsService.API.Entities.Form_Submission", b =>
                {
                    b.HasOne("SimpleFormsService.API.Entities.Form_Template", "Form_Template")
                        .WithMany("Submissions")
                        .HasForeignKey("Form_TemplateTemplate_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form_Template");
                });

            modelBuilder.Entity("SimpleFormsService.API.Entities.Form_Template", b =>
                {
                    b.Navigation("Submissions");
                });
#pragma warning restore 612, 618
        }
    }
}