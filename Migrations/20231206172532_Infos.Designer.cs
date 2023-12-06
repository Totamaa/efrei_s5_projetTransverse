﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projet.Models.Context;

#nullable disable

namespace Projet.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20231206172532_Infos")]
    partial class Infos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Projet.Models.Entity.CyberharceleurEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Pseudo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cyberharceleurs");
                });

            modelBuilder.Entity("Projet.Models.Entity.DossierEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CyberharceleurId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CyberharceleurId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Dossiers");
                });

            modelBuilder.Entity("Projet.Models.Entity.PreuveEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contenu")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DossierId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TypePreuveId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DossierId");

                    b.HasIndex("TypePreuveId");

                    b.ToTable("Preuves");
                });

            modelBuilder.Entity("Projet.Models.Entity.TypePreuveEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TypePreuves");
                });

            modelBuilder.Entity("Projet.Models.Entity.UtilisateurEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Pseudo")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Pseudo")
                        .IsUnique();

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("Projet.Models.Entity.DossierEntity", b =>
                {
                    b.HasOne("Projet.Models.Entity.CyberharceleurEntity", "CyberharceleurEntity")
                        .WithMany("Dossiers")
                        .HasForeignKey("CyberharceleurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projet.Models.Entity.UtilisateurEntity", "UtilisateurEntity")
                        .WithMany("Dossiers")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CyberharceleurEntity");

                    b.Navigation("UtilisateurEntity");
                });

            modelBuilder.Entity("Projet.Models.Entity.PreuveEntity", b =>
                {
                    b.HasOne("Projet.Models.Entity.DossierEntity", "DossierEntity")
                        .WithMany("Preuves")
                        .HasForeignKey("DossierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projet.Models.Entity.TypePreuveEntity", "TypePreuveEntity")
                        .WithMany("Preuves")
                        .HasForeignKey("TypePreuveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DossierEntity");

                    b.Navigation("TypePreuveEntity");
                });

            modelBuilder.Entity("Projet.Models.Entity.CyberharceleurEntity", b =>
                {
                    b.Navigation("Dossiers");
                });

            modelBuilder.Entity("Projet.Models.Entity.DossierEntity", b =>
                {
                    b.Navigation("Preuves");
                });

            modelBuilder.Entity("Projet.Models.Entity.TypePreuveEntity", b =>
                {
                    b.Navigation("Preuves");
                });

            modelBuilder.Entity("Projet.Models.Entity.UtilisateurEntity", b =>
                {
                    b.Navigation("Dossiers");
                });
#pragma warning restore 612, 618
        }
    }
}
