using AuthorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorAPI.Repository
{
    public class AuthorDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; } = null!;

        public AuthorDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasKey(author => author.Id);

            modelBuilder.Entity<Author>()
                .Property(author => author.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Author>()
                .Property(author => author.FirstName)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(author => author.SecondName)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(author => author.Birthday)
                .IsRequired();
        }
    }
}
