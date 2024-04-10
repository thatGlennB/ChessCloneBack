using ChessCloneBack.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChessCloneBack.DAL
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Unique UserName property in database
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}
