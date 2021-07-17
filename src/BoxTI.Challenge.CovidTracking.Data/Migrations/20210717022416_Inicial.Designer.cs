﻿// <auto-generated />
using System;
using BoxTI.Challenge.CovidTracking.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoxTI.Challenge.CovidTracking.Data.Migrations
{
    [DbContext(typeof(CovidTrackingContext))]
    [Migration("20210717022416_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("BoxTI.Challenge.CovidTracking.Models.Entities.InfoCovid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<ulong>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("ActiveCases")
                        .HasColumnType("int(15)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime");

                    b.Property<string>("NewCases")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("NewDeaths")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<int>("TotalCases")
                        .HasColumnType("int(15)");

                    b.Property<int>("TotalDeaths")
                        .HasColumnType("int(15)");

                    b.Property<int>("TotalRecovered")
                        .HasColumnType("int(15)");

                    b.HasKey("Id");

                    b.ToTable("InfoCovid");
                });
#pragma warning restore 612, 618
        }
    }
}