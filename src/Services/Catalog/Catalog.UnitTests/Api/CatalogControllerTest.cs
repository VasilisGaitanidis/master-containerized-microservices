using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Controllers;
using Catalog.Application.Dtos.Requests;
using Catalog.Application.Dtos.Responses;
using Catalog.Application.UseCases.Commands.CreateCatalogItem;
using Catalog.Application.UseCases.Commands.DeleteCatalogItem;
using Catalog.Application.UseCases.Commands.UpdateCatalogItem;
using Catalog.Application.UseCases.Queries.GetCatalogItemById;
using Catalog.Application.UseCases.Queries.GetCatalogItems;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Catalog.UnitTests.Api
{
    public class CatalogControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        public CatalogControllerTest()
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
            var actionResult = await catalogController.GetCatalogItemsAsync();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status200OK, ((OkObjectResult)actionResult.Result).StatusCode);
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
            var actionResult = await catalogController.GetCatalogItemAsync(fakeCatalogItemId);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status200OK, ((OkObjectResult)actionResult.Result).StatusCode);
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
            var actionResult = await catalogController.CreateCatalogItemAsync(fakeDto);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(actionResult);
            Assert.Equal(StatusCodes.Status201Created, ((CreatedAtRouteResult)actionResult).StatusCode);
            Assert.Equal("GetCatalogItem", ((CreatedAtRouteResult)actionResult).RouteName);
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
            var actionResult = await catalogController.UpdateCatalogItemAsync(fakeCatalogItemId, fakeDto);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(StatusCodes.Status200OK, ((OkObjectResult)actionResult).StatusCode);
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
            var actionResult = await catalogController.DeleteCatalogItemAsync(fakeCatalogItemId);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(StatusCodes.Status200OK, ((OkObjectResult)actionResult).StatusCode);
        }

        #endregion
    }
}