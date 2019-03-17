using System;
using Microsoft.EntityFrameworkCore;
using REST.DataAccess.Configurations;
using REST.DataAccess.Models;

namespace REST.DataAccess.Contexts
{
    public class PersonContext : DbContext
    {
        public DbSet<DbPerson> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=RestHW;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());

            modelBuilder.Entity<DbPerson>().HasData(
                new DbPerson
                {
                    Id = 1,
                    Name = "John",
                    Surname = "Doe",
                    BirthDate = DateTime.Now - TimeSpan.FromDays(400)
                });
            modelBuilder.Entity<DbPerson>().HasData(
                new DbPerson
                {
                    Id = 2,
                    Name = "Tom",
                    Surname = "Moto",
                    BirthDate = DateTime.Now - TimeSpan.FromDays(788)
                });
            modelBuilder.Entity<DbPerson>().HasData(
                new DbPerson
                {
                    Id = 3,
                    Name = "Fred",
                    Surname = "Smitt",
                    BirthDate = DateTime.Now - TimeSpan.FromDays(400)
                });

        }
    }
}