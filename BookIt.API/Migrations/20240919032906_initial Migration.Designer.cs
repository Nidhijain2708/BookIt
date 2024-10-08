﻿// <auto-generated />
using System;
using BookIt.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookIt.API.Migrations
{
    [DbContext(typeof(BookItDbContext))]
    [Migration("20240919032906_initial Migration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookIt.API.Models.Domain.Booking", b =>
                {
                    b.Property<Guid>("booking_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeOnly>("booking_time")
                        .HasColumnType("time");

                    b.Property<Guid>("event_id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("event_id1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("number_of_tickets")
                        .HasColumnType("int");

                    b.Property<string>("payment_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("total_price")
                        .HasColumnType("float");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("booking_id");

                    b.HasIndex("event_id1");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BookIt.API.Models.Domain.Event", b =>
                {
                    b.Property<Guid>("event_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("artist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("available_tickets")
                        .HasColumnType("int");

                    b.Property<int>("capacity")
                        .HasColumnType("int");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("date")
                        .HasColumnType("date");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("end_time")
                        .HasColumnType("time");

                    b.Property<string>("event_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<TimeOnly>("start_time")
                        .HasColumnType("time");

                    b.HasKey("event_id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("BookIt.API.Models.Domain.Booking", b =>
                {
                    b.HasOne("BookIt.API.Models.Domain.Event", "Event")
                        .WithMany()
                        .HasForeignKey("event_id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });
#pragma warning restore 612, 618
        }
    }
}
