using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using EletronicPartsCatalog.Features.Followers;
using EletronicPartsCatalog.Features.Profiles;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Followers
{
    public class DeleteTest
    {
        [Fact]
        public async Task Delete_Follower_Inexistent_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();
            var mockedIProfileReader = new Mock<IProfileReader>();
            mockedCurrentUserAcessor.Setup(mcua => mcua.GetCurrentUsername()).Returns("test");
            var sut = new Delete.QueryHandler(mockedContext, mockedCurrentUserAcessor.Object, mockedIProfileReader.Object);
            var message = new Delete.Command("slug");

            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }

    }
}
