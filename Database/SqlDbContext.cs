using CRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Database
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Note> Notes{ get; set; }
        public DbSet<CreationDate> Dates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreationDate>()
                .HasOne<Note>(d => d.Note)
                .WithOne(p => p.CreationDate)
                .HasForeignKey<Note>(p => p.CreationDateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
