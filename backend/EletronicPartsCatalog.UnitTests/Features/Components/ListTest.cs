using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Features.Components;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Components
{
    public class ListTest
    {
        [Fact]
        public async Task Get_Components_Inexistent_ShouldReturn_ZeroComponents()
        {
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();
            mockedCurrentUserAcessor.Setup(mcua => mcua.GetCurrentUsername()).Returns("test");
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var sut = new List.QueryHandler(mockedContext,mockedCurrentUserAcessor.Object);
            var message = new List.Query(String.Empty, String.Empty, "searchTerm", limit: 10, offset: 0);

            var result = await sut.Handle(message, CancellationToken.None);

            Assert.Equal(0, result.ComponentsCount);
        }
    }
}
