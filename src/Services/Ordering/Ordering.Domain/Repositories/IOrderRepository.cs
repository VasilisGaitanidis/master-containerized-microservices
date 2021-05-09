using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Ordering.Domain.Entities;

namespace Ordering.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<Order> GetOrderAsync(Guid id);

        Task<IEnumerable<Order>> GetOrdersAsync();

        Task<Order> AddAsync(Order order);

        void Update(Order order);

        void Delete(Order order);
    }
}