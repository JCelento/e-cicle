using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Features.Components;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Components
{
    public class DetailsTest
    {
        [Fact]
        public async Task Get_Component_Inexistent_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();
            var sut = new Details.QueryHandler(mockedContext);
            var message = new Details.Query("slug");

            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }

    }
}

