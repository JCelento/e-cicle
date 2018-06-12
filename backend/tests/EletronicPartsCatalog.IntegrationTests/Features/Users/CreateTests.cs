using System.Linq;
using System.Threading.Tasks;
using EletronicPartsCatalog.Features.Users;
using EletronicPartsCatalog.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EletronicPartsCatalog.IntegrationTests.Features.Users
{
    public class CreateTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Create_User()
        {
            var command = new EletronicPartsCatalog.Features.Users.Create.Command()
            {
                User = new Create.UserData()
                {
                    Email = "email",
                    Password = "password",
                    Username = "username"
                }
            };

            await SendAsync(command);

            var created = await ExecuteDbContextAsync(db => db.Persons.Where(d => d.Email == command.User.Email).SingleOrDefaultAsync());

            Assert.NotNull(created);
            Assert.Equal(created.Hash, new PasswordHasher().Hash("password", created.Salt));
        }
    }
}