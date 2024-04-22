using ChessCloneBack.DAL.Repositories;
using ChessCloneBack.Test.Infrastructure;

namespace ChessCloneBack.Test.Tests
{
    public class UserRepositoryTest:IClassFixture<TestDatabaseFixture>
    {
        private TestDatabaseFixture _fixture;
        public UserRepositoryTest(TestDatabaseFixture fixture) 
        {
            _fixture = fixture;
        }

        // test - data not modified
        [Fact]
        public void GetSeededValue_ReturnsObject()
        {
            using (DatabaseContext context = _fixture.CreateContext()) 
            {
                UserRepository repo = new(context);
                User? result = repo.GetByUsername(TestUtil.SeedUser.UserName);
                Assert.NotNull(result);
            }
        }
        [Fact]
        public void GetUnseededValue_ReturnsNull()
        {
            using (DatabaseContext context = _fixture.CreateContext())
            {
                UserRepository repo = new(context);
                User? result = repo.GetByUsername(TestUtil.NewUserName);
                Assert.Null(result);
            }
        }

        // test - data modified
        // not sure that I need this test...
        [Fact]
        public void AddUserWithExistingName_Fails()
        {
            using (DatabaseContext context = _fixture.CreateContext())
            {
                // start transaction to make sure changes are not committed to DB and don't interfere with other tests
                context.Database.BeginTransaction();

                // modifications of database go here
                UserRepository repo = new(context);
                Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() =>
                    repo.Add(TestUtil.SeedUser));

                context.Database.RollbackTransaction();
            }
        }
        [Fact]
        public void AddUserWithNewName_Passes()
        {
            using (DatabaseContext context = _fixture.CreateContext())
            {
                // start transaction to make sure changes are not committed to DB and don't interfere with other tests
                context.Database.BeginTransaction();

                // modifications of database go here
                UserRepository repo = new(context);
                repo.Add(TestUtil.SecondUser);
                
                // clear change tracker to avoid conflicts between actually-existing object in database and object in memory
                context.ChangeTracker.Clear();

                // readonly events modified above go here
                User? result = context.Users.SingleOrDefault(o => o.UserName == "Curly");
                Assert.Equal([0], result?.PasswordSaltHash);

                context.Database.RollbackTransaction();
            }
        }
    }
}