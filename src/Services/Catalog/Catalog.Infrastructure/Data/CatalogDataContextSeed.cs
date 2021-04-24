using Catalog.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class CatalogDataContextSeed
    {
        #region Demo entities

        private static readonly CatalogType _demoCatalogType1 = new CatalogType("Demo Catalog Type 1");
        private static readonly CatalogType _demoCatalogType2 = new CatalogType("Demo Catalog Type 2");

        public static readonly CatalogItem _demoCatalogItem1 = new CatalogItem("Demo name 1", "Demo description 1", 25, 50, _demoCatalogType1.Id);
        public static readonly CatalogItem _demoCatalogItem2 = new CatalogItem("Demo name 2", "Demo description 2", 19.99M, 90, _demoCatalogType2.Id);

        #endregion

        public static async Task SeedAsync(CatalogDataContext catalogDataContext, ILogger<CatalogDataContext> logger)
        {
            if (!catalogDataContext.CatalogTypes.Any())
            {
                catalogDataContext.CatalogTypes.AddRange(GetPreconfiguredCatalogTypes());

                await catalogDataContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(CatalogDataContext).Name);
            }

            if (!catalogDataContext.CatalogItems.Any())
            {
                catalogDataContext.CatalogItems.AddRange(GetPreconfiguredCatalogItems());

                await catalogDataContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(CatalogDataContext).Name);
            }
        }

        private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                _demoCatalogType1,
                _demoCatalogType2
            };
        }

        private static IEnumerable<CatalogItem> GetPreconfiguredCatalogItems()
        {
            return new List<CatalogItem>
            {
                _demoCatalogItem1,
                _demoCatalogItem2
            };
        }
    }
}
