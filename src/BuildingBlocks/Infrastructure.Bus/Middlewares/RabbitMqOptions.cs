namespace Infrastructure.Bus.Middlewares
{
    public class RabbitMqOptions
    {
        /// <summary>
        /// The host name of the broker
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The virtual host to use
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// The username for the connection
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password for the connection
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The queue name for the receive endpoint
        /// </summary>
        public string QueueName { get; set; }
    }
}