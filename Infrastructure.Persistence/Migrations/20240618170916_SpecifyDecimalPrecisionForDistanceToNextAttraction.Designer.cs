﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240618170916_SpecifyDecimalPrecisionForDistanceToNextAttraction")]
    partial class SpecifyDecimalPrecisionForDistanceToNextAttraction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Auth.Domain.RefreshToken", b =>
                {
                    b.Property<Guid>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Expired")
                        .HasColumnType("datetime2");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUser", b =>
                {
                    b.Property<Guid>("ApplicationUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastSingInDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ApplicationUserId");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserApplicationUserRole", b =>
                {
                    b.Property<int>("ApplicationUserRoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ApplicationUserRoleId", "ApplicationUserId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("ApplicationUserApplicationUserRole");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserRole", b =>
                {
                    b.Property<int>("ApplicationUserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationUserRoleId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ApplicationUserRoleId");

                    b.ToTable("ApplicationUserRoles");
                });

            modelBuilder.Entity("Travels.Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Street")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Travels.Domain.Attraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GeoLocationId")
                        .HasColumnType("int");

                    b.Property<bool>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("NumberOfVisitors")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("GeoLocationId")
                        .IsUnique()
                        .HasFilter("[GeoLocationId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Attractions");
                });

            modelBuilder.Entity("Travels.Domain.AttractionFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttractionId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ValueRating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttractionId");

                    b.HasIndex("UserId");

                    b.ToTable("AttractionFeedback");
                });

            modelBuilder.Entity("Travels.Domain.AttractionInRoute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttractionId")
                        .HasColumnType("int");

                    b.Property<decimal>("DistanceToNextAttraction")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VisitDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AttractionId");

                    b.HasIndex("RouteId");

                    b.ToTable("AttractionInRoutes");
                });

            modelBuilder.Entity("Travels.Domain.GeoLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("Latitude")
                        .HasMaxLength(50)
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasMaxLength(50)
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("GeoLocations");
                });

            modelBuilder.Entity("Travels.Domain.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Travels.Domain.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("Travels.Domain.TourFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.HasIndex("UserId");

                    b.ToTable("TourFeedback");
                });

            modelBuilder.Entity("Travels.Domain.WorkSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttractionId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("CloseTime")
                        .HasColumnType("time");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("OpenTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("AttractionId");

                    b.ToTable("WorkSchedules");
                });

            modelBuilder.Entity("Auth.Domain.RefreshToken", b =>
                {
                    b.HasOne("Core.Users.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserApplicationUserRole", b =>
                {
                    b.HasOne("Core.Users.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Users.Domain.ApplicationUserRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("ApplicationUserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Travels.Domain.Attraction", b =>
                {
                    b.HasOne("Travels.Domain.Address", "Address")
                        .WithMany("Attractions")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travels.Domain.GeoLocation", "GeoLocation")
                        .WithOne("Attraction")
                        .HasForeignKey("Travels.Domain.Attraction", "GeoLocationId");

                    b.HasOne("Core.Users.Domain.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("GeoLocation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Travels.Domain.AttractionFeedback", b =>
                {
                    b.HasOne("Travels.Domain.Attraction", "Attraction")
                        .WithMany("AttractionFeedback")
                        .HasForeignKey("AttractionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Users.Domain.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Attraction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Travels.Domain.AttractionInRoute", b =>
                {
                    b.HasOne("Travels.Domain.Attraction", "Attraction")
                        .WithMany("AttractionsInRoutes")
                        .HasForeignKey("AttractionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Travels.Domain.Route", "Route")
                        .WithMany("AttractionsInRoutes")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Attraction");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Travels.Domain.Route", b =>
                {
                    b.HasOne("Core.Users.Domain.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Travels.Domain.Tour", b =>
                {
                    b.HasOne("Travels.Domain.Route", "Route")
                        .WithMany("Tours")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Travels.Domain.TourFeedback", b =>
                {
                    b.HasOne("Travels.Domain.Tour", "Tour")
                        .WithMany("TourFeedback")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Users.Domain.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tour");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Travels.Domain.WorkSchedule", b =>
                {
                    b.HasOne("Travels.Domain.Attraction", "Attraction")
                        .WithMany("WorkSchedules")
                        .HasForeignKey("AttractionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attraction");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUser", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Travels.Domain.Address", b =>
                {
                    b.Navigation("Attractions");
                });

            modelBuilder.Entity("Travels.Domain.Attraction", b =>
                {
                    b.Navigation("AttractionFeedback");

                    b.Navigation("AttractionsInRoutes");

                    b.Navigation("WorkSchedules");
                });

            modelBuilder.Entity("Travels.Domain.GeoLocation", b =>
                {
                    b.Navigation("Attraction")
                        .IsRequired();
                });

            modelBuilder.Entity("Travels.Domain.Route", b =>
                {
                    b.Navigation("AttractionsInRoutes");

                    b.Navigation("Tours");
                });

            modelBuilder.Entity("Travels.Domain.Tour", b =>
                {
                    b.Navigation("TourFeedback");
                });
#pragma warning restore 612, 618
        }
    }
}
