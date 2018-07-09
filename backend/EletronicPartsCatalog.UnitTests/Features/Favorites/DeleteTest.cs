using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using EletronicPartsCatalog.Features.Favorites;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Favorites
{
    public class DeleteTest
    {
        [Fact]
        public async Task Delete_Favorite_Inexistent_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();
            mockedCurrentUserAcessor.Setup(mcua => mcua.GetCurrentUsername()).Returns("test");
            var sut = new Delete.QueryHandler(mockedContext, mockedCurrentUserAcessor.Object);
            var message = new Delete.Command("slug");

            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }

    }
}
