using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Features.Profiles;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Profiles
{
    public class DetailsTest
    {
        [Fact]
        public async Task Get_Profile_Inexistent_ShouldReturn_NoResult()
        {
            var mockedProfileReader = new Mock<IProfileReader>();
            var sut = new Details.QueryHandler(mockedProfileReader.Object);
            var message = new Details.Query
            {
                Username = "user"
            };
            var result = await sut.Handle(message, CancellationToken.None);
            Assert.Null(result);
        }
    }
}

