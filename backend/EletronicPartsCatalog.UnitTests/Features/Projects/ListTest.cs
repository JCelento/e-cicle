using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Features.Projects;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Projects
{
    public class ListTest
    {
        [Fact]
        public async Task Get_Projects_Inexistent_ShouldReturn_ZeroProjects()
        {
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();
            mockedCurrentUserAcessor.Setup(mcua => mcua.GetCurrentUsername()).Returns("test");
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var sut = new List.QueryHandler(mockedContext,mockedCurrentUserAcessor.Object);
            var message = new List.Query(tag: String.Empty, author: String.Empty, favorited: String.Empty, search: "searchTerm", component: String.Empty, limit: 10, offset: 0);

            var result = await sut.Handle(message, CancellationToken.None);

            Assert.Equal(0, result.ProjectsCount);
        }
    }
}
