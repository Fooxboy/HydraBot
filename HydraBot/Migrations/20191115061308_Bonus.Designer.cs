﻿// <auto-generated />
using HydraBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HydraBot.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20191115061308_Bonus")]
    partial class Bonus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

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
