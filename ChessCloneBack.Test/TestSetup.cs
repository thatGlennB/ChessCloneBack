using ChessCloneBack.BLL;
using ChessCloneBack.BLL.Interfaces;
using ChessCloneBack.DAL.Interfaces;
using ChessCloneBack.Test.Mocks;
using ChessCloneBack.Test.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChessCloneBack.Test
{
    public class TestSetup
    {
        public TestSetup()
        {
            ServiceCollection serviceCollection = new();
            // TODO: set basepath to API basepath
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                     path: "appsettings.json",
                     optional: false,
                     reloadOnChange: true)
               .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddTransient<IUserRepository, MockUserRepository>();
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider ServiceProvider { get; private set; }
    }
}
