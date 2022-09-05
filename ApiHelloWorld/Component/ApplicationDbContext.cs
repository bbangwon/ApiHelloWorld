using ApiHelloWorld.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiHelloWorld.Component
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Point>? Points { get; set; }
        public DbSet<PointLog>? PointLogs { get; set; }
        public DbSet<Note>? Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PointLog>()
                .Property(pl => pl.Created)
                .HasDefaultValueSql("getdate()");
        }

    }
}
