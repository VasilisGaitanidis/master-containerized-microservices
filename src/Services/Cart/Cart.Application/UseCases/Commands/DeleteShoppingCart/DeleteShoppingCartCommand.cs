using MediatR;

namespace Cart.Application.UseCases.Commands.DeleteShoppingCart
{
    public class DeleteShoppingCartCommand : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="username">The username.</param>
        public DeleteShoppingCartCommand(string username)
        {
            Username = username;
        }

        /// <summary>
        /// The username.
        /// </summary>
        public string Username { get; }
    }
}