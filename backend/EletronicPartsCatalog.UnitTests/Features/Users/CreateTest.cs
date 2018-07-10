using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoFixture;
using System.Threading.Tasks;
using AutoMapper;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Features.Users;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Security;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Users
{
    public class CreateTest
    {
        [Fact]
        public async Task Create_User_WithInvalidInput_ShouldReturn_InvalidOperationException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedHasher = new Mock<IPasswordHasher>();
            var mockedJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            var mockedMapper = new Mock<IMapper>();
            var sut = new Create.Handler(mockedContext, mockedHasher.Object, mockedJwtTokenGenerator.Object, mockedMapper.Object);

            var message = new Create.Command
            {
                User = new Create.UserData()
                {
                    Username = "name",
                    Email = "mail@mail.com",
                    Password = "pass",
                    PasswordConfirmation = "another pass"
                }
            };

            await Assert.ThrowsAsync<NullReferenceException>(() => sut.Handle(message, CancellationToken.None));
        }
    }
}
