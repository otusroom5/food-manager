﻿// <auto-generated />
using System;
using FoodUserNotifier.Infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodUserNotifier.Infrastructure.Repositories.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240615024718_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FoodUserNotifier.Infrastructure.Repositories.Contract.DeliveryReportDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<Guid>("NotificationId")
                        .HasMaxLength(50)
                        .HasColumnType("uuid");

                    b.Property<bool>("Success")
                        .HasMaxLength(10)
                        .HasColumnType("boolean");

                    b.HasKey("Id")
                        .HasName("deliveryreport");

                    b.ToTable("deliveryreport");
                });

            modelBuilder.Entity("FoodUserNotifier.Infrastructure.Repositories.Contract.RecepientDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("RecepientId")
                        .HasMaxLength(100)
                        .HasColumnType("uuid");

                    b.Property<string>("Telegram")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id")
                        .HasName("recepientid");

                    b.ToTable("recepient");
                });
#pragma warning restore 612, 618
        }
    }
}
