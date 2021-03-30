using System;
using System.Threading.Tasks;
using Cart.Application.Dtos.Responses;
using Cart.Application.UseCases.Queries.GetShoppingCartByUsername;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(ShoppingCartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCartAsync(string username)
            => Ok(await _mediator.Send(new GetShoppingCartByUsernameQuery(username)));
    }
}
