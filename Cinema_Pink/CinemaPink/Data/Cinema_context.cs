using CinemaPink.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CinemaPink.Data
{
    public class Cinema_context : DbContext
    {
        public Cinema_context(DbContextOptions<Cinema_context> options) : base(options)
        {
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Film>().ToTable("Film");
        //    modelBuilder.Entity<Projection>().ToTable("Projection");
        //    modelBuilder.Entity<Reservation>().ToTable("Reservation");
        //    modelBuilder.Entity<Room>().ToTable("Room");
        //    modelBuilder.Entity<Seat>().ToTable("Seat");

        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
