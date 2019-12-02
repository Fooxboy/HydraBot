﻿// <auto-generated />
using HydraBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HydraBot.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("HydraBot.Models.Car", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Engine")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Health")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsUnderRepair")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<long>("Power")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Weight")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("HydraBot.Models.Contribution", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CountDay")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Money")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Contributions");
                });

            modelBuilder.Entity("HydraBot.Models.Engine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<long>("Power")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Weight")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("HydraBot.Models.Gang", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Creator")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Members")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Gangs");
                });

            modelBuilder.Entity("HydraBot.Models.Garage", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cars")
                        .HasColumnType("TEXT");

                    b.Property<string>("Engines")
                        .HasColumnType("TEXT");

                    b.Property<long>("Fuel")
                        .HasColumnType("INTEGER");

                    b.Property<long>("GarageModelId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPhone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<long>("ParkingPlaces")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<long>("SelectCar")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Garages");
                });

            modelBuilder.Entity("HydraBot.Models.Report", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnswerMessage")
                        .HasColumnType("TEXT");

                    b.Property<long>("FromId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAnswered")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<long>("ModeratorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("HydraBot.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Access")
                        .HasColumnType("INTEGER");

                    b.Property<long>("BonusDay")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BusinessIds")
                        .HasColumnType("TEXT");

                    b.Property<long>("DonateMoney")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DriverLicense")
                        .HasColumnType("TEXT");

                    b.Property<long>("Gang")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAvailbleBonus")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Money")
                        .HasColumnType("INTEGER");

                    b.Property<long>("MoneyInBank")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Prefix")
                        .HasColumnType("TEXT");

                    b.Property<long>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TgId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimeBan")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimeBonus")
                        .HasColumnType("INTEGER");

                    b.Property<long>("VkId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
