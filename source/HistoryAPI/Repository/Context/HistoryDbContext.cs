using HistoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryAPI.Repository
{
    public class HistoryDbContext : DbContext
    {
        public DbSet<Visitor> Visitors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<HistoryRecord> History { get; set; } = null!;

        public HistoryDbContext(DbContextOptions options) : base(options) { }

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

            modelBuilder.Entity<HistoryRecord>(opt =>
            {
                opt.HasKey(history => history.Id);

                opt.Property(history => history.Id)
                    .UseIdentityColumn();

                opt.HasOne(history => history.Book)
                    .WithMany(book => book.BorrowedHistory)
                    .HasForeignKey(history => history.BookId)
                    .OnDelete(DeleteBehavior.ClientCascade);

                opt.HasOne(history => history.Visitor)
                    .WithMany(visitor => visitor.BorrowedHistory)
                    .HasForeignKey(history => history.VisitorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
