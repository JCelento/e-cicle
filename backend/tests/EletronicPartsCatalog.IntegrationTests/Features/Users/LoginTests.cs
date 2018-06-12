using System;
using System.Threading.Tasks;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Features.Users;
using EletronicPartsCatalog.Infrastructure.Security;
using Xunit;

namespace EletronicPartsCatalog.IntegrationTests.Features.Users
{
    public class LoginTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Login()
        {
            var salt = Guid.NewGuid().ToByteArray();
            var person = new Person
            {
                Username = "username",
                Email = "email",
                Hash = new PasswordHasher().Hash("password", salt),
                Salt = salt
            };
            await InsertAsync(person);

            var command = new EletronicPartsCatalog.Features.Users.Login.Command()
            {
                User = new Login.UserData()
                {
                    Email = "email",
                    Password = "password"
                }
            };

            var user = await SendAsync(command);

            Assert.NotNull(user?.User);
            Assert.Equal(user.User.Email, command.User.Email);
            Assert.Equal("username", user.User.Username);
            Assert.NotNull(user.User.Token);
        }
    }
}