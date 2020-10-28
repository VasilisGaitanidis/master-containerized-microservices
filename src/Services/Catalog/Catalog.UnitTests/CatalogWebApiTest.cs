using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.Api.Controllers;
using Catalog.Application.Dtos.Requests;
using Catalog.Application.Dtos.Responses;
using Catalog.Application.UseCases.CreateCatalogItem;
using Catalog.Application.UseCases.DeleteCatalogItem;
using Catalog.Application.UseCases.GetCatalogItemById;
using Catalog.Application.UseCases.GetCatalogItems;
using Catalog.Application.UseCases.UpdateCatalogItem;
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
                .Returns(Task.FromResult(Unit.Value));

            // Act
            var catalogController = new CatalogController(_mediatorMock.Object);
            var actionResult = await catalogController.UpdateCatalogItemAsync(fakeCatalogItemId, fakeDto) as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, actionResult?.StatusCode);
        }

        #endregion

        #region DeleteCatalogItemAsync

        [Fact]
        public async Task Delete_catalog_item_success()
        {
            // Arrange
            var fakeCatalogItemId = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteCatalogItemCommand>(), default))
                .Returns(Task.FromResult(Unit.Value));

            // Act
            var catalogController = new CatalogController(_mediatorMock.Object);
            var actionResult = await catalogController.DeleteCatalogItemAsync(fakeCatalogItemId) as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, actionResult?.StatusCode);
        }

        #endregion
    }
}