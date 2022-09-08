using Microsoft.EntityFrameworkCore;
using VisitorAPI.Models;

namespace VisitorAPI.Repository
{
    public class VisitorDbContext : DbContext
    {
        public DbSet<Visitor> Visitors { get; set; } = null!;

        public VisitorDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visitor>(opt =>
            {
                opt.HasKey(visitor => visitor.Id);

                opt.Property(visitor => visitor.Id)
                    .UseIdentityColumn();

                opt.Ignore(visitor => visitor.Age);
            });
        }
    }
}
