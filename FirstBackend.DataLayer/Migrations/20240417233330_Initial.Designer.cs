﻿// <auto-generated />
using System;
using FirstBackend.DataLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FirstBackend.DataLayer.Migrations
{
    [DbContext(typeof(MainerLxContext))]
    [Migration("20240417233330_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FirstBackend.Core.Dtos.DeviceDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<int>("DeviceType")
                        .HasColumnType("integer")
                        .HasColumnName("device_type");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.HasKey("Id")
                        .HasName("pk_devices");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_devices_owner_id");

                    b.ToTable("devices", (string)null);
                });

            modelBuilder.Entity("FirstBackend.Core.Dtos.OrderDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_orders_customer_id");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("FirstBackend.Core.Dtos.UserDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Mail")
                        .HasColumnType("text")
                        .HasColumnName("mail");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("FirstBackend.Core.Dtos.DeviceDto", b =>
                {
                    b.HasOne("FirstBackend.Core.Dtos.UserDto", "Owner")
                        .WithMany("Devices")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("fk_devices_users_owner_id");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("FirstBackend.Core.Dtos.OrderDto", b =>
                {
                    b.HasOne("FirstBackend.Core.Dtos.UserDto", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("fk_orders_users_customer_id");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FirstBackend.Core.Dtos.UserDto", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
