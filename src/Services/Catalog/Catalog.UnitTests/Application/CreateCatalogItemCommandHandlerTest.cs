using System;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Application.Dtos.Responses;
using Catalog.Application.UseCases.CreateCatalogItem;
using Catalog.Domain.Models;
using Catalog.Domain.Repositories;
using Moq;
using Xunit;

namespace Catalog.UnitTests.Application
{
    public class CreateCatalogItemCommandHandlerTest
    {
        private readonly Mock<ICatalogItemRepository> _catalogItemRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateCatalogItemCommandHandlerTest()
        {
            _catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_Methods_Should_Be_Called_Once()
        {
            // Arrange
            var fakeCommand = new CreateCatalogItemCommand("fakeName", "fakeDescription", 10, 20, Guid.NewGuid());
            _catalogItemRepositoryMock.Setup(x => x.AddAsync(It.IsAny<CatalogItem>()))
                .Returns(Task.FromResult<CatalogItem>(null)).Verifiable();
            _mapperMock.Setup(x => x.Map<CatalogItemDto>(It.IsAny<CatalogItem>()))
                .Returns(new CatalogItemDto()).Verifiable();

            // Act
            var handler = new CreateCatalogItemCommandHandler(_catalogItemRepositoryMock.Object, _mapperMock.Object);
            await handler.Handle(fakeCommand, default);

            // Assert
            _catalogItemRepositoryMock.Verify(x => x.AddAsync(It.IsAny<CatalogItem>()), Times.Once, "AddAsync must be called only once.");
            _mapperMock.Verify(x => x.Map<CatalogItemDto>(It.IsAny<CatalogItem>()), Times.Once, "Map must be called only once.");
        }
    }
}