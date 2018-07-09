using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Features.Comments;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Comments
{
    public class ListTest
    {
        [Fact]
        public async Task Get_Comments_Inexistent_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var sut = new List.QueryHandler(mockedContext);
            var message = new List.Query("commentSlug");

            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }
    }
}
