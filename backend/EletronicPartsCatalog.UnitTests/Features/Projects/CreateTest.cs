using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoFixture;
using System.Threading.Tasks;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Features.Projects;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Projects
{
    public class AddTest
    {
        [Fact]
        public async Task Create_Project_LoggedWithValidInput_Should_CreateProject()
        {
            var fixture = new Fixture();
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();
            mockedCurrentUserAcessor.Setup(mcua => mcua.GetCurrentUsername()).Returns("test");

            var sut = new Create.Handler(mockedContext, mockedCurrentUserAcessor.Object);

            var compentlist = fixture.Create<string[]>();
            var taglist = fixture.Create<string[]>();

            var message = new Create.Command
            {
                Project = new Create.ProjectData
                {
                    ComponentList = compentlist,
                    Title = "Title",
                    Description = "Description",
                    Body = "Body",
                    TagList = taglist
                }
            };

            var result = await sut.Handle(message, CancellationToken.None);

            Assert.Equal(result.Project.Title, message.Project.Title);
        }

        [Fact]
        public async Task Create_Project_NotLogged_WithValidInput_ShouldReturn_InvalidOperationException()
        {
            var fixture = new Fixture();
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();

            var sut = new Create.Handler(mockedContext, mockedCurrentUserAcessor.Object);

            var compentlist = fixture.Create<string[]>();
            var taglist = fixture.Create<string[]>();

            var message = new Create.Command
            {
                Project = new Create.ProjectData
                {
                    ComponentList = compentlist,
                    Title = "Title",
                    Description = "Description",
                    Body = "Body",
                    TagList = taglist
                }
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Handle(message, CancellationToken.None));
        }
    }
}
