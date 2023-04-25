using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbAccess
{
    public class AMDbContext : DbContext
    {
        public AMDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
    }
}
