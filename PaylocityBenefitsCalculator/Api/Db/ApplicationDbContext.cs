using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Db
{
    /// <summary>
    /// We are using EF with InMemory Storage to simulate database.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Dependent> Dependents { get; set; } = default!;


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Seeding needed data.
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new List<Employee>
            {
                new()
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                },
                new()
                {
                    Id = 2,
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10),
                },
                new()
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17),
                }
            });

            modelBuilder.Entity<Dependent>().HasData(new List<Dependent>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Spouse",
                    LastName = "Morant",
                    Relationship = Relationship.Spouse,
                    DateOfBirth = new DateTime(1998, 3, 3),
                    EmployeeId = 2,
                },
                new()
                {
                    Id = 2,
                    FirstName = "Child1",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2020, 6, 23),
                    EmployeeId = 2,
                },
                new()
                {
                    Id = 3,
                    FirstName = "Child2",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2021, 5, 18),
                    EmployeeId = 2,
                },
                new()
                {
                    Id = 4,
                    FirstName = "DP",
                    LastName = "Jordan",
                    Relationship = Relationship.DomesticPartner,
                    DateOfBirth = new DateTime(1974, 1, 2),
                    EmployeeId = 3,
                }
            });
        }
    }
}