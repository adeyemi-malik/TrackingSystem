﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mints.DLayer.Models;

namespace Mints.DLayer.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20181122192223_Initial4")]
    partial class Initial4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932");

            modelBuilder.Entity("Mints.DLayer.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<byte[]>("Picture");

                    b.Property<int>("Status");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("Tag")
                        .IsUnique();

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Mints.DLayer.Models.AnimalTracker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimalId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int>("Status");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("TrackerId");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("TrackerId");

                    b.ToTable("AnimalTrackers");
                });

            modelBuilder.Entity("Mints.DLayer.Models.ApiClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiKeyHash")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("CallBackUrl")
                        .IsRequired()
                        .HasMaxLength(750);

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ClientName")
                        .IsUnique();

                    b.ToTable("ApiClients");
                });

            modelBuilder.Entity("Mints.DLayer.Models.AppLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<string>("LogEntry")
                        .IsRequired();

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("Key");

                    b.ToTable("AppLogs");
                });

            modelBuilder.Entity("Mints.DLayer.Models.Farmer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(800);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Farmers");
                });

            modelBuilder.Entity("Mints.DLayer.Models.FarmerAnimal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimalId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int>("FarmerId");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("FarmerId");

                    b.ToTable("FarmerAnimals");
                });

            modelBuilder.Entity("Mints.DLayer.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimalTrackerId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("AnimalTrackerId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Mints.DLayer.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Mints.DLayer.Models.Tracker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int>("Status");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("Tag")
                        .IsUnique();

                    b.ToTable("Trackers");
                });

            modelBuilder.Entity("Mints.DLayer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("HashSalt")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(750);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Mints.DLayer.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int>("RoleId");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Mints.DLayer.Models.AnimalTracker", b =>
                {
                    b.HasOne("Mints.DLayer.Models.Animal", "Animal")
                        .WithMany("AnimalTrackers")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Mints.DLayer.Models.Tracker", "Tracker")
                        .WithMany("AnimalTrackers")
                        .HasForeignKey("TrackerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Mints.DLayer.Models.Farmer", b =>
                {
                    b.HasOne("Mints.DLayer.Models.User", "User")
                        .WithOne("Farmer")
                        .HasForeignKey("Mints.DLayer.Models.Farmer", "UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Mints.DLayer.Models.FarmerAnimal", b =>
                {
                    b.HasOne("Mints.DLayer.Models.Animal", "Animal")
                        .WithMany("FarmerAnimals")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Mints.DLayer.Models.Farmer", "Farmer")
                        .WithMany("FarmerAnimals")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Mints.DLayer.Models.Location", b =>
                {
                    b.HasOne("Mints.DLayer.Models.AnimalTracker", "AnimalTracker")
                        .WithMany("Locations")
                        .HasForeignKey("AnimalTrackerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Mints.DLayer.Models.UserRole", b =>
                {
                    b.HasOne("Mints.DLayer.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Mints.DLayer.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
