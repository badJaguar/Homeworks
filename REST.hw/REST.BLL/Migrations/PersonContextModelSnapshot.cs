﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using REST.DataAccess;
using REST.DataAccess.Contexts;

namespace REST.DataAccess.Migrations
{
    [DbContext(typeof(PersonContext))]
    partial class PersonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("REST.BLL.DbPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("cln_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnName("cln_birth_date");

                    b.Property<string>("Name")
                        .HasColumnName("cln_name");

                    b.Property<string>("Surname")
                        .HasColumnName("cln_surname");

                    b.HasKey("Id");

                    b.ToTable("tbl_persons");

                    b.HasData(
                        new { Id = 1, BirthDate = new DateTime(2018, 2, 10, 19, 58, 48, 192, DateTimeKind.Local), Name = "John", Surname = "Doe" },
                        new { Id = 2, BirthDate = new DateTime(2017, 1, 18, 19, 58, 48, 194, DateTimeKind.Local), Name = "Tom", Surname = "Moto" },
                        new { Id = 3, BirthDate = new DateTime(2018, 2, 10, 19, 58, 48, 194, DateTimeKind.Local), Name = "Fred", Surname = "Smitt" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
