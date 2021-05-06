namespace Ordering.Application.Dtos.Requests
{
    public class BuyerDto
    {
        /// <summary>
        /// The buyer's fist name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The buyer's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The buyer's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The buyer's country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The buyer's state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The buyer's zip code.
        /// </summary>
        public string ZipCode { get; set; }
    }
}