using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

        [HttpGet("catalog-items")]
        [ProducesResponseType(typeof(IEnumerable<CatalogItemResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCatalogItemsAsync()
        {
            var result = await _mediator.Send(new GetCatalogItemsQuery());

            return Ok(result);
        }

        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CatalogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
