﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TryAspNetCore.Api.Core.Context;

namespace TryAspNetCore.Api.Migrations
{
    [DbContext(typeof(EventContext))]
    [Migration("20190306083652_BaseEntityAddedCreatedAndUpdatedInformation")]
    partial class BaseEntityAddedCreatedAndUpdatedInformation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TryAspNetCore.Api.Domain.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EventId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Title");

                    b.Property<Guid>("UpdadetBy");

                    b.Property<DateTime>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.EventRegistration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EventRegistrationId");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<Guid>("EventId");

                    b.Property<Guid>("UpdadetBy");

                    b.Property<DateTime>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventRegistration");
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.UserToken", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.EventRegistration", b =>
                {
                    b.HasOne("TryAspNetCore.Api.Domain.Event")
                        .WithMany("Registrations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.RoleClaim", b =>
                {
                    b.HasOne("TryAspNetCore.Api.Domain.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.UserClaim", b =>
                {
                    b.HasOne("TryAspNetCore.Api.Domain.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.UserLogin", b =>
                {
                    b.HasOne("TryAspNetCore.Api.Domain.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.UserRole", b =>
                {
                    b.HasOne("TryAspNetCore.Api.Domain.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TryAspNetCore.Api.Domain.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TryAspNetCore.Api.Domain.UserToken", b =>
                {
                    b.HasOne("TryAspNetCore.Api.Domain.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
