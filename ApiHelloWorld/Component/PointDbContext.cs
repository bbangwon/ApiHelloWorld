using Microsoft.EntityFrameworkCore;

namespace ApiHelloWorld.Component
{
    public class PointDbContext : DbContext
    {
        public PointDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Point>? Points { get; set; }
        public DbSet<PointLog>? PointLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PointLog>()
                .Property(pl => pl.Created)
                .HasDefaultValueSql("getdate()");
        }

    }
}
