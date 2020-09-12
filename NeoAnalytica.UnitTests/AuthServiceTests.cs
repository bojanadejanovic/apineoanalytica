using Moq;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NeoAnalytica.UnitTests
{

    public class AuthServiceTests
    {

        [Fact]
        public async Task GetAllUsers()
        {
            // Arrange
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { Email = "bojana.dejanovic@outlook.com", Id = 1},
                new ApplicationUser() { Email = "ivana.petkovic@test.com", Id = 2}
            };

            Mock<IDatabaseConnectionFactory> connectionFactoryMock = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();
            db.Insert(users);
            connectionFactoryMock.Setup(c => c.GetDbConnection(DatabaseConnectionName.DefaultConnection)).Returns(db.OpenConnection());
            var result = await new AuthService(connectionFactoryMock.Object, null).GetAllAsync();

            Assert.Equal(users.Count, result.Count());

        }
    }
}
