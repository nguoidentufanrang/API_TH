using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using webAPIThucHanh.Models.Domain;

namespace webAPIThucHanh.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
		public DbSet<Image> Images { get; set; }
		public DbSet<Authors> Author { get; set; }
        public DbSet<Book_Author> BookAuthors { get; set; }
        public DbSet<Books> Book { get; set; }
        public DbSet<Publishers> Publisher { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book_Author>()
.           HasOne(b => b.Book)
.           WithMany(ba => ba.Book_Authors)
.           HasForeignKey(bi => bi.BookId);
            builder.Entity<Book_Author>()
            .HasOne(b => b.Author)
            .WithMany(ba => ba.Book_Authors)
            .HasForeignKey(bi => bi.AuthorId);
        }


    }
}
