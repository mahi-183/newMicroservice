﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryManager.DBContext;

namespace RepositoryManager.Migrations
{
    [DbContext(typeof(AuthenticationContext))]
    [Migration("20190924063515_fundooNotes")]
    partial class fundooNotes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommanLayer.Model.CollaboratorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<int>("NoteId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Collaborator");
                });

            modelBuilder.Entity("CommanLayer.Model.LabelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("LabelName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("CommanLayer.Model.NotesLabelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<int>("LabelId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("NoteId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("NotesLabel");
                });

            modelBuilder.Entity("CommanLayer.Model.NotesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<bool>("IsPin");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<DateTime?>("Reminder");

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.Property<int>("noteType");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
