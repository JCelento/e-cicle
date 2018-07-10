using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using EletronicPartsCatalog.Features.Users;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.Infrastructure.Security;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Users
{
    public class EditTest
    {
        [Fact]
        public async Task Edit_Inexistent_User_WithValidInput_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedHasher = new Mock<IPasswordHasher>();
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();
            mockedCurrentUserAcessor.Setup(mcua => mcua.GetCurrentUsername()).Returns("test");
            var mockedMapper = new Mock<IMapper>();

            var sut = new Edit.Handler(mockedContext, mockedHasher.Object, mockedCurrentUserAcessor.Object, mockedMapper.Object);

            var message = new Edit.Command
            {
                User = new Edit.UserData
                {
                    Username = "name",
                    Bio = "some bio",
                    Email = "mail@address.com",
                    Password = "pass"
                }
            };

            var result =  await sut.Handle(message, CancellationToken.None);

            Assert.Null(result.User);
        }
    }
}
