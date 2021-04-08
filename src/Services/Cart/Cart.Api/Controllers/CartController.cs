using System;
using System.Threading.Tasks;
using Cart.Application.Dtos.Requests;
using Cart.Application.Dtos.Responses;
using Cart.Application.UseCases.Commands.DeleteShoppingCart;
using Cart.Application.UseCases.Commands.UpdateShoppingCart;
using Cart.Application.UseCases.Queries.GetShoppingCartByUsername;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{username}")]
        [ProducesResponseType(typeof(ShoppingCartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCartAsync(string username)
            => Ok(await _mediator.Send(new GetShoppingCartByUsernameQuery(username)));

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(ShoppingCartDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartDto>> UpdateShoppingCartAsync([FromBody] UpdateShoppingCartDto dto)
            => Ok(await _mediator.Send(new UpdateShoppingCartCommand(dto.Username, dto.Items)));

        [HttpDelete]
        [Route("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteShoppingCartAsync(string username)
            => Ok(await _mediator.Send(new DeleteShoppingCartCommand(username)));
    }
}
