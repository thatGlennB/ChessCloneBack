using ChessCloneBack.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChessCloneBack.DAL
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
    }
}
