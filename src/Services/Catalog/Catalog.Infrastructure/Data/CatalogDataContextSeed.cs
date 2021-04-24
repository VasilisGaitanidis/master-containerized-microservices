using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Data
{
    public class CatalogDataContextSeed
    {
        #region Demo entities

        private static readonly CatalogType DemoCatalogType1 = new CatalogType("Demo Catalog Type 1");
        private static readonly CatalogType DemoCatalogType2 = new CatalogType("Demo Catalog Type 2");

        private static readonly CatalogItem DemoCatalogItem1 = new CatalogItem("Demo name 1", "Demo description 1", 25, 50, DemoCatalogType1.Id);
        private static readonly CatalogItem DemoCatalogItem2 = new CatalogItem("Demo name 2", "Demo description 2", 19.99M, 90, DemoCatalogType2.Id);

        #endregion

        public static async Task SeedAsync(CatalogDataContext catalogDataContext, ILogger<CatalogDataContextSeed> logger)
        {
            if (!catalogDataContext.CatalogTypes.Any())
            {
                catalogDataContext.CatalogTypes.AddRange(GetPreconfiguredCatalogTypes());

                await catalogDataContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", nameof(CatalogDataContext));
            }

            if (!catalogDataContext.CatalogItems.Any())
            {
                catalogDataContext.CatalogItems.AddRange(GetPreconfiguredCatalogItems());

                await catalogDataContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", nameof(CatalogDataContext));
            }
        }

        private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {
                DemoCatalogType1,
                DemoCatalogType2
            };
        }

        private static IEnumerable<CatalogItem> GetPreconfiguredCatalogItems()
        {
            return new List<CatalogItem>
            {
                DemoCatalogItem1,
                DemoCatalogItem2
            };
        }
    }
}
