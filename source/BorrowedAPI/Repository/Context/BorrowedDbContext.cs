using BorrowedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowedAPI.Repository
{
    public class BorrowedDbContext : DbContext
    {
        public DbSet<Visitor> Visitors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BorrowedRecord> BorrowedRecords { get; set; } = null!;

        public BorrowedDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(opt =>
            {
                opt.HasKey(book => book.Id);

                opt.Property(book => book.Id)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Visitor>(opt =>
            {
                opt.HasKey(visitor => visitor.Id);

                opt.Property(visitor => visitor.Id)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<BorrowedRecord>(opt =>
            {
                opt.HasKey(borrowed => borrowed.Id);

                opt.Property(borrowed => borrowed.Id)
                    .UseIdentityColumn();

                opt.HasOne(borrowed => borrowed.Book)
                    .WithMany(book => book.BorrowedHistory)
                    .HasForeignKey(borrowed => borrowed.BookId)
                    .OnDelete(DeleteBehavior.ClientCascade);

                opt.HasOne(borrowed => borrowed.Visitor)
                    .WithMany(visitor => visitor.BorrowedHistory)
                    .HasForeignKey(borrowed => borrowed.VisitorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
