using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.Api.Application.Commands;
using Catalog.Api.Application.Dtos.Requests;
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

        #region GetCatalogItemsAsync

        [Fact]
        public async Task Get_catalog_items_success()
        {
            // Arrange
            var fakeDynamicResult = Enumerable.Empty<CatalogItemDto>();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetCatalogItemsQuery>(), default))
                .Returns(Task.FromResult(fakeDynamicResult));

            // Act
            var catalogController = new CatalogController(_mediatorMock.Object);
            var actionResult = await catalogController.GetCatalogItemsAsync() as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, actionResult?.StatusCode);
        }

        #endregion

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

        #region CreateCatalogItemAsync

        [Fact]
        public async Task Create_catalog_item_success()
        {
            // Arrange
            var fakeDto = new CreateCatalogItemDto();
            var fakeDynamicResult = new CatalogItemDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateCatalogItemCommand>(), default))
                .Returns(Task.FromResult(fakeDynamicResult));

            // Act
            var catalogController = new CatalogController(_mediatorMock.Object);
            var actionResult = await catalogController.CreateCatalogItemAsync(fakeDto) as CreatedAtRouteResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.Created, actionResult?.StatusCode);
            Assert.Equal("GetCatalogItem", actionResult?.RouteName);
        }

        #endregion

        #region UpdateCatalogItemAsync

        [Fact]
        public async Task Update_catalog_item_success()
        {
            // Arrange
            var fakeCatalogItemId = Guid.NewGuid();
            var fakeDto = new UpdateCatalogItemDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateCatalogItemCommand>(), default))
                .Returns(Task.FromResult(true));

            // Act
            var catalogController = new CatalogController(_mediatorMock.Object);
            var actionResult = await catalogController.UpdateCatalogItemAsync(fakeCatalogItemId, fakeDto) as OkResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, actionResult?.StatusCode);
        }

        [Fact]
        public async Task Update_catalog_item_bad_request()
        {
            // Arrange
            var fakeCatalogItemId = Guid.NewGuid();
            var fakeDto = new UpdateCatalogItemDto();
            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateCatalogItemCommand>(), default))
                .Returns(Task.FromResult(false));

            // Act
            var catalogController = new CatalogController(_mediatorMock.Object);
            var actionResult = await catalogController.UpdateCatalogItemAsync(fakeCatalogItemId, fakeDto) as BadRequestResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, actionResult?.StatusCode);
        }

        #endregion
    }
}