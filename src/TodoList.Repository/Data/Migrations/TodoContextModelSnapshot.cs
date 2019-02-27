﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoList.Repository.Data;

namespace TodoList.Repository.Data.Migrations.SqlServerMigrations
{
    [DbContext(typeof(TodoContext))]
    partial class TodoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoList.Domain.Models.Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("Creator");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Label")
                        .HasMaxLength(255);

                    b.Property<DateTime>("OffTime");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<Guid?>("TodoTypeId");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<Guid>("Updater");

                    b.HasKey("Id");

                    b.HasIndex("TodoTypeId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TodoList.Domain.Models.TodoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("Creator");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Todo")
                        .IsRequired();

                    b.Property<Guid>("TodoId");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<Guid>("Updater");

                    b.HasKey("Id");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("TodoList.Domain.Models.TodoType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("Creator");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<Guid>("Updater");

                    b.HasKey("Id");

                    b.ToTable("TodoTypes");
                });

            modelBuilder.Entity("TodoList.Domain.Models.Todo", b =>
                {
                    b.HasOne("TodoList.Domain.Models.TodoType", "TodoType")
                        .WithMany()
                        .HasForeignKey("TodoTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
