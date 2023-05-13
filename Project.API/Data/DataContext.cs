using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Shared.Entities;

namespace Project.API.Data
{
    public class DataContext : IdentityDbContext<User>

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Specie> Species { get; set; }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Disease> Diseases { get; set; }

        public DbSet<Zoo> Zoos { get; set; }

        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("Name", "CountryId").IsUnique();
            modelBuilder.Entity<City>().HasIndex("Name", "StateId").IsUnique();
            modelBuilder.Entity<Specie>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Animal>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Disease>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Zoo>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Manager>().HasIndex(c => c.Name).IsUnique();
        }
    }
    
}
