using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.DataAccess
{
    public class PostgresSqlContext : DbContext
    {
        public DbSet<CatAlumno> CatAlumno { get; set; }
        
        public PostgresSqlContext(DbContextOptions<PostgresSqlContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}