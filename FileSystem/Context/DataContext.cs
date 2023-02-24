using FileSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FileSystem.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions) { }
        DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>().ToTable("Catalog");
        }

    }
}
