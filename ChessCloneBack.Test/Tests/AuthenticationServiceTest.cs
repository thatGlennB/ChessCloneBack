using ChessCloneBack.BLL;
using ChessCloneBack.BLL.Interfaces;
using ChessCloneBack.DAL.Interfaces;
using ChessCloneBack.Test.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ChessCloneBack.Test.Tests
{
    public class AuthenticationServiceTest
    {
        private readonly IAuthenticationService _service;
        private readonly IServiceProvider _serviceProvider;
        public AuthenticationServiceTest() 
        {
            TestSetup testSetup = new();
            _serviceProvider = testSetup.ServiceProvider;
            IAuthenticationService? service = _serviceProvider.GetService<IAuthenticationService>();
            if (service == null) 
            {
                throw new NullReferenceException($"Dependency injection returns null - the service provider of {nameof(testSetup)} does not contain a reference for {nameof(IAuthenticationService)}");
            }
            _service = service;
            
        }
        [Fact]
        public void RedundantUser_IsNameAvailable_ReturnsFalse ()
        {
            bool result = _service.IsNameAvailable(TestUtil.ExistingUserName);
            Assert.False(result );
        }
        [Fact]
        public void NewUser_IsNameAvailable_ReturnsTrue()
        {
            bool result = _service.IsNameAvailable(TestUtil.NewUserName);
            Assert.True(result);
        }
        [Fact]
        public void RedundantUser_AddNewCredentials_ThrowsException() 
        {
            Assert.Throws<ArgumentException>(() =>
               _service.AddNewCredentials(TestUtil.ExistingUserName,TestUtil.FakePassword));
        }
        [Fact]
        public void NewUser_AddNewCredentials_NoException()
        {
            Exception exc = Record.Exception(() =>
               _service.AddNewCredentials(TestUtil.NewUserName, TestUtil.FakePassword));
            Assert.Null(exc);
        }
        [Fact]
        public void CorrectPassword_IsValidCredentials_ReturnsTrue() 
        {
            string outputMessage;
            bool result = _service.IsValidCredentials(TestUtil.ExistingUserName, TestUtil.FakePassword, out outputMessage);
            Assert.True(result);
        }
        [Fact]
        public void IncorrectPassword_IsValidCredentials_ReturnsFalse()
        {
            string outputMessage;
            bool result = _service.IsValidCredentials(TestUtil.ExistingUserName, "wrong", out outputMessage);
            Assert.False(result);
        }
    }
}
