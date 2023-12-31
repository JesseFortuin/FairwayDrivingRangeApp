﻿// <auto-generated />
using System;
using FairwayDrivingRange.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FairwayDrivingRange.Infrastructure.Migrations
{
    [DbContext(typeof(FairwayContext))]
    [Migration("20230904111210_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FairwayDrivingRange.Domain.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateBooked")
                        .HasColumnType("datetime2");

                    b.Property<int>("Lane")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("FairwayDrivingRange.Domain.Entities.CustomerInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomerInformation");
                });

            modelBuilder.Entity("FairwayDrivingRange.Domain.Entities.GolfClub", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookingId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("GolfClubs");
                });

            modelBuilder.Entity("FairwayDrivingRange.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("BookingPrice")
                        .HasColumnType("float");

                    b.Property<double>("ClubPrice")
                        .HasColumnType("float");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("FairwayDrivingRange.Domain.Entities.Booking", b =>
                {
                    b.HasOne("FairwayDrivingRange.Domain.Entities.CustomerInformation", "Customer")
                        .WithMany("Booking")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FairwayDrivingRange.Domain.Entities.GolfClub", b =>
                {
                    b.HasOne("FairwayDrivingRange.Domain.Entities.Booking", "Booking")
                        .WithMany("Clubs")
                        .HasForeignKey("BookingId");

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("FairwayDrivingRange.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("FairwayDrivingRange.Domain.Entities.CustomerInformation", "CustomerInformation")
                        .WithMany("Transaction")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerInformation");
                });

            modelBuilder.Entity("FairwayDrivingRange.Domain.Entities.Booking", b =>
                {
                    b.Navigation("Clubs");
                });

            modelBuilder.Entity("FairwayDrivingRange.Domain.Entities.CustomerInformation", b =>
                {
                    b.Navigation("Booking");

                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
