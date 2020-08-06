using System;
using Domain.Core.Models;

namespace Catalog.Domain.Models
{
    public class CatalogType : Entity<Guid>
    {
        private string _name;

        public CatalogType(string name) : base(Guid.NewGuid())
        {
            _name = name;
        }
    }
}