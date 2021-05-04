using System;
using System.Collections.Generic;
using Domain.Models;

namespace Ordering.Domain.Entities
{
    /// <summary>
    /// The buyer domain entity.
    /// </summary>
    public class Buyer : Entity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Buyer"/>.
        /// </summary>
        /// <param name="firstName">The buyer's fist name.</param>
        /// <param name="lastName">The buyer's last name.</param>
        /// <param name="email">The buyer's email.</param>
        /// <param name="country">The buyer's country.</param>
        /// <param name="state">The buyer's state.</param>
        /// <param name="zipCode">The buyer's zip code.</param>
        public Buyer(string firstName, string lastName, string email, string country, string state, string zipCode) : base(Guid.NewGuid())
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email;
            Country = country ?? throw new ArgumentNullException(nameof(country));
            State = state ?? throw new ArgumentNullException(nameof(state));
            ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
        }

        /// <summary>
        /// The buyer's fist name.
        /// </summary>
        public string FirstName { get; protected set; }

        /// <summary>
        /// The buyer's last name.
        /// </summary>
        public string LastName { get; protected set; }

        /// <summary>
        /// The buyer's email.
        /// </summary>
        public string Email { get; protected set; }

        /// <summary>
        /// The buyer's country.
        /// </summary>
        public string Country { get; protected set; }

        /// <summary>
        /// The buyer's state.
        /// </summary>
        public string State { get; protected set; }

        /// <summary>
        /// The buyer's zip code.
        /// </summary>
        public string ZipCode { get; protected set; }

        /// <summary>
        /// The buyer's orders.
        /// </summary>
        public IEnumerable<Order> Orders { get; protected set; }
    }
}