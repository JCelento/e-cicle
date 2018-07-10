using System.Threading;
using AutoFixture;
using System.Threading.Tasks;
using EletronicPartsCatalog.Features.Components;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Components
{
    public class CreateTest
    {
        [Fact]
        public async Task Create_Component_LoggedWithValidInput_Should_CreateComponent()
        {
            var fixture = new Fixture();
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();
            mockedCurrentUserAcessor.Setup(mcua => mcua.GetCurrentUsername()).Returns("test");

            var sut = new Create.Handler(mockedContext, mockedCurrentUserAcessor.Object);

            var wheretofinditlist = fixture.Create<string[]>();

            var message = new Create.Command
            {
                Component = new Create.ComponentData()
                {
                    Name = "Component",
                    Description = "Description",
                    WhereToFindItList = wheretofinditlist
                }
            };

            var result = await sut.Handle(message, CancellationToken.None);

            Assert.Equal(result.Component.Description, message.Component.Description);
        }

    }
}
