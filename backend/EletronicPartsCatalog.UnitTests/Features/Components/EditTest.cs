using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using EletronicPartsCatalog.Features.Components;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Components
{
    public class EditTest
    {
        [Fact]
        public async Task Edit_Inexistent_Project_WithValidInput_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();

            var sut = new Edit.Handler(mockedContext, mockedCurrentUserAcessor.Object);

            var message = new Edit.Command
            {
                Component = new Edit.ComponentData()
                {
                    Description = "Description",
                },
                Slug = "slug"
            };

            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }
    }
}
