
using Microsoft.EntityFrameworkCore;

namespace ChessCloneBack.Test
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ChessClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Persist Security Info=True";
        private static readonly object _lock = new();
        private static bool _databaseInitialized;
        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (DatabaseContext context = CreateContext()) 
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        context.Users.Add(new User
                        {
                            UserName = "Larry",
                            PasswordSaltHash = [1]
                        });



                        context.SaveChanges();
                    }
                }
            }
        }
        public DatabaseContext CreateContext()
            => new DatabaseContext(
                new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlServer(ConnectionString).Options);
    }
}
