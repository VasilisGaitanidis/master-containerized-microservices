using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.Api.Application.Commands;
using Catalog.Api.Application.Dtos;
using Catalog.Api.Application.Queries;
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
        [ProducesResponseType(typeof(IEnumerable<CatalogItemResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCatalogItemsAsync()
            => Ok(await _mediator.Send(new GetCatalogItemsQuery()));

        [HttpGet("items/{id:Guid}", Name = "GetCatalogItem")]
        [ProducesResponseType(typeof(CatalogItemResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCatalogItemAsync(Guid id)
        {
            var result = await _mediator.Send(new GetCatalogItemByIdQuery(id));

            return result == null ? NotFound() : (IActionResult)Ok(result);
        }

        [HttpPost("items")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCatalogItemAsync(CreateCatalogItemCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtRoute("GetCatalogItem", new { id = result.Id }, result);
        }

        [HttpPut("items/{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCatalogItemAsync(Guid id, [FromBody] UpdateCatalogItemCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

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
