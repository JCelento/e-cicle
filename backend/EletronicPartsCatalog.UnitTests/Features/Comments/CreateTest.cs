using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoFixture;
using System.Threading.Tasks;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Features.Comments;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Comments
{
    public class CreateTest
    {
        [Fact]
        public async Task Create_Comment_NotLogged_WithValidInput_ShouldReturn_InvalidOperationException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var mockedCurrentUserAcessor = new Mock<ICurrentUserAccessor>();

            var sut = new Create.Handler(mockedContext, mockedCurrentUserAcessor.Object);

            var message = new Create.Command
            {
                Comment = new Create.CommentData()
                {
                    Body = "Body"
                }
            };

            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }
    }
}
