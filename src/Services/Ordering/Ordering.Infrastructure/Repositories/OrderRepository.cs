using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderingDataContext _context;

        public OrderRepository(OrderingDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Order> GetOrderAsync(Guid id)
        {
            var order = await _context.Orders
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var orders = await _context.Orders
                .ToListAsync();

            return orders;
        }

        public async Task<Order> AddAsync(Order order)
        {
            var entityEntry = await _context.Orders.AddAsync(order);

            return entityEntry.Entity;
        }

        public void Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;

            _context.Update(order);
        }

        public void Delete(Order order)
        {
            _context.Entry(order).State = EntityState.Deleted;

            _context.Orders.Remove(order);
        }
    }
}
