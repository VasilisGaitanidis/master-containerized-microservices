using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("items")]
        [ProducesResponseType(typeof(IEnumerable<CatalogItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCatalogItemsAsync()
            => Ok(await _mediator.Send(new GetCatalogItemsQuery()));

        [HttpGet("items/{id:Guid}", Name = "GetCatalogItem")]
        [ProducesResponseType(typeof(CatalogItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCatalogItemAsync(Guid id)
           => Ok(await _mediator.Send(new GetCatalogItemByIdQuery(id)));

        [HttpPost("items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCatalogItemAsync([FromBody] CreateCatalogItemDto dto)
        {
            var result = await _mediator.Send(new CreateCatalogItemCommand(dto.Name, dto.Description, dto.Price, dto.Stock, dto.CatalogTypeId));

            return CreatedAtRoute("GetCatalogItem", new { id = result.Id }, result);
        }

        [HttpPut("items/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCatalogItemAsync(Guid id, [FromBody] UpdateCatalogItemDto dto)
            => Ok(await _mediator.Send(new UpdateCatalogItemCommand(id, dto.Name, dto.Description, dto.Price, dto.Stock, dto.CatalogTypeId)));

        [HttpDelete("items/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCatalogItemAsync(Guid id)
            => Ok(await _mediator.Send(new DeleteCatalogItemCommand(id)));
    }
}
