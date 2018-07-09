using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Features.Users;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Users
{
    public class DetailsTest
    {
        [Fact]
        public async Task Get_User_Inexistent_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedMapper = new Mock<IMapper>();
            var sut = new Details.QueryHandler(mockedContext, mockedMapper.Object);
            var message = new Details.Query
            {
                Username = "inexistent user"
            };
            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }

    }
}

