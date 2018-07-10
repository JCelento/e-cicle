using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Features.Tags;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Tags
{
    public class ListTest
    {
        [Fact]
        public async Task Get_Tags_Inexistent_ShouldReturn_NoTags()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var sut = new List.QueryHandler(mockedContext);
            var message = new List.Query();

            var result = await sut.Handle(message, CancellationToken.None);

            Assert.Empty(result.Tags);
        }
    }
}
