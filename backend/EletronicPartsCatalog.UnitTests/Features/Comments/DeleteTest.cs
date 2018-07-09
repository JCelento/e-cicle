using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using EletronicPartsCatalog.Features.Comments;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Comments
{
    public class DeleteTest
    {
        [Fact]
        public async Task Delete_Comment_Inexistent_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var sut = new Delete.QueryHandler(mockedContext);
            var message = new Delete.Command("comment", 1);

            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }

    }
}
