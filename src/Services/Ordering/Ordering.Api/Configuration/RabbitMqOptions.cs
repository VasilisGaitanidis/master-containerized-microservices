namespace Ordering.Api.Configuration
{
    public class RabbitMqOptions
    {
        public string Host { get; set; }

        public string VirtualHost { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}