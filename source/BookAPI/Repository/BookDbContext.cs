using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repository
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<Author> Authors { get; set; } = null!;

        public BookDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(opt =>
            {
                opt.HasKey(book => book.Id);

                opt.Property(book => book.Id)
                    .UseIdentityColumn();

                opt.Property(book => book.Title)
                    .IsRequired();

                opt.Property(book => book.PublicationDate)
                    .IsRequired();

                opt.Property(book => book.AuthorId)
                    .IsRequired();

                opt.HasOne(book => book.Author)
                    .WithMany(author => author.Books)
                    .HasForeignKey(book => book.AuthorId);
            });

            modelBuilder.Entity<Author>(opt =>
            {
                opt.HasKey(author => author.Id);

                opt.Property(author => author.Id)
                    .ValueGeneratedNever();

                opt.Property(author => author.FirstName)
                    .IsRequired();

                opt.Property(author => author.SecondName)
                    .IsRequired();

                opt.HasMany(author => author.Books)
                    .WithOne(book => book.Author)
                    .HasForeignKey(book => book.AuthorId);
            });
        }
    }
}
