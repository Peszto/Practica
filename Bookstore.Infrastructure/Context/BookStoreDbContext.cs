
using System.Linq;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Context
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //     => options.UseSqlServer("Server=localhost,1433; Database=Bookstore;User=sa; Password=Pajti2004; TrustServerCertificate=True;");
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(150)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}