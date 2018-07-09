using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoFixture;
using System.Threading.Tasks;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Features.Followers;
using EletronicPartsCatalog.Features.Profiles;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Followers
{
    public class AddTest
    {

        [Fact]
        public async Task Add_Follower_NotLogged_WithValidInput_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();
            var mockedIProfileReader = new Mock<IProfileReader>();

            var sut = new Add.QueryHandler(mockedContext, mockedCurrentUserAcessor.Object, mockedIProfileReader.Object);

            var message = new Add.Command("slug");

            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }
    }
}
