using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.Application.Commands;
using Catalog.Application.Dtos.Requests;
using Catalog.Application.Dtos.Responses;
using Catalog.Application.Queries;
using MediatR;
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
        [ProducesResponseType(typeof(IEnumerable<CatalogItemDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCatalogItemsAsync()
            => Ok(await _mediator.Send(new GetCatalogItemsQuery()));

        [HttpGet("items/{id:Guid}", Name = "GetCatalogItem")]
        [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCatalogItemAsync(Guid id)
        {
            var result = await _mediator.Send(new GetCatalogItemByIdQuery(id));

            return result == null ? NotFound() : (IActionResult)Ok(result);
        }

        [HttpPost("items")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCatalogItemAsync([FromBody] CreateCatalogItemDto dto)
        {
            var result = await _mediator.Send(new CreateCatalogItemCommand(dto.Name, dto.Description, dto.Price, dto.Stock, dto.CatalogTypeId));

            return CreatedAtRoute("GetCatalogItem", new { id = result.Id }, result);
        }

        [HttpPut("items/{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCatalogItemAsync(Guid id, [FromBody] UpdateCatalogItemDto dto)
        {
            var result = await _mediator.Send(new UpdateCatalogItemCommand(id, dto.Name, dto.Description, dto.Price, dto.Stock, dto.CatalogTypeId));

            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("items/{id:Guid}")]
        public void Delete(int id)
        {
        }
    }
}
