using System;
using Domain.Models;

namespace Catalog.Domain.Entities
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