using System;
using System.Net;
using System.Threading.Tasks;
using Catalog.Api.Application.Dtos.Responses;
using Catalog.Api.Application.Queries;
using Catalog.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Catalog.UnitTests
{
    public class CatalogWebApiTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        public CatalogWebApiTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        #region GetCatalogItemAsync

        [Fact]
        public async Task Get_catalog_item_success()
        {
            // Arrange
            var fakeCatalogItemId = Guid.Empty;
            var fakeDynamicResult = new CatalogItemDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetCatalogItemByIdQuery>(), default))
                .Returns(Task.FromResult(fakeDynamicResult));

            // Act
            var catalogController = new CatalogController(_mediatorMock.Object);
            var actionResult = await catalogController.GetCatalogItemAsync(fakeCatalogItemId) as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, actionResult?.StatusCode);
        }

        [Fact]
        public async Task Get_catalog_item_not_found()
        {
            // Arrange
            var fakeCatalogItemId = Guid.Empty;
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetCatalogItemByIdQuery>(), default))
                .Returns(Task.FromResult((CatalogItemDto)null));

            // Act
            var catalogController = new CatalogController(_mediatorMock.Object);
            var actionResult = await catalogController.GetCatalogItemAsync(fakeCatalogItemId) as NotFoundResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, actionResult?.StatusCode);
        }

        #endregion
    }
}