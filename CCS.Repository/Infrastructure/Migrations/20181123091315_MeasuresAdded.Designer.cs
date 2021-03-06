﻿// <auto-generated />
using System;
using CCS.Repository.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CCS.Repository.Infrastructure.Migrations
{
    [DbContext(typeof(StationContext))]
    [Migration("20181123091315_MeasuresAdded")]
    partial class MeasuresAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

            modelBuilder.Entity("CCS.Repository.Entities.Measure", b =>
                {
                    b.Property<int>("MeasureId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Humidity");

                    b.Property<int>("Location");

                    b.Property<decimal>("Temperature");

                    b.Property<DateTime>("Time");

                    b.HasKey("MeasureId");

                    b.ToTable("Measures","station");
                });

            modelBuilder.Entity("CCS.Repository.Entities.Setting", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InnerTemperatureMax");

                    b.Property<int>("InnerTemperatureMin");

                    b.Property<int>("Mode");

                    b.Property<int>("OuterTemperatureMax");

                    b.Property<int>("OuterTemperatureMin");

                    b.Property<DateTime>("ScheduleStar");

                    b.Property<DateTime>("ScheduleStop");

                    b.HasKey("SettingId");

                    b.ToTable("Settings","station");
                });
#pragma warning restore 612, 618
        }
    }
}
