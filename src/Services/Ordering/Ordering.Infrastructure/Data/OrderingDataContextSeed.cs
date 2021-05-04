using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderingDataContextSeed
    {
        #region Demo entities

        private static readonly Buyer DemoBuyer1 = new Buyer("Demo first name 1", "Demo last name 1", "Demo email 1", "Demo country 1", "Demo state 1", "Demo zip code 1");
        private static readonly Buyer DemoBuyer2 = new Buyer("Demo first name 2", "Demo last name 2", "Demo email 2", "Demo country 2", "Demo state 2", "Demo zip code 2");

        private static readonly OrderItem DemoOrderItem1 = new OrderItem(1, 15.5M, "Demo1");
        private static readonly OrderItem DemoOrderItem2 = new OrderItem(2, 10M, "Demo2");

        private static readonly Order Order1 = new Order("Demo username 1", 30.5M, "Demo shipping address 1", DemoBuyer1, new List<OrderItem> { DemoOrderItem1, DemoOrderItem2 });

        #endregion

        public static async Task SeedAsync(OrderingDataContext catalogDataContext, ILogger<OrderingDataContextSeed> logger)
        {
            if (!catalogDataContext.Buyers.Any())
            {
                catalogDataContext.Buyers.AddRange(GetPreconfiguredBuyers());

                await catalogDataContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", nameof(OrderingDataContext));
            }

            if (!catalogDataContext.OrderItems.Any())
            {
                catalogDataContext.OrderItems.AddRange(GetPreconfiguredOrderItems());

                await catalogDataContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", nameof(OrderingDataContext));
            }

            if (!catalogDataContext.Orders.Any())
            {
                catalogDataContext.Orders.AddRange(GetPreconfiguredOrders());

                await catalogDataContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", nameof(OrderingDataContext));
            }
        }

        private static IEnumerable<Buyer> GetPreconfiguredBuyers()
        {
            return new List<Buyer>
            {
                DemoBuyer1,
                DemoBuyer2
            };
        }

        private static IEnumerable<OrderItem> GetPreconfiguredOrderItems()
        {
            return new List<OrderItem>
            {
                DemoOrderItem1,
                DemoOrderItem2
            };
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                Order1
            };
        }
    }
}
