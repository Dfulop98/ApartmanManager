

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using System.Runtime.CompilerServices;

namespace DataAccessLayer.DbAccess
{
    public class AMDbContext : DbContext
    {

        public AMDbContext(DbContextOptions options) : base (options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Reservation> Reservations { get; set; }

    }

}
