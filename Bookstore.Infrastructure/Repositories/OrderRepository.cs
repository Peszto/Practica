using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using BookStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Orders>, IOrderRepository
    {
        public OrderRepository(BookStoreDbContext context) : base(context) { }

        public override async Task<List<Orders>> GetAll()
        {
            return await Db.Orders.AsNoTracking()
                .Include(c=>c.Client)
                .Include(b=>b.Book)
                .OrderBy(o=>o.Id)
                .ToListAsync();
        }

        public override async Task<Orders> GetById(int id)
        {
            return await Db.Orders.AsNoTracking()
                .Include(c=>c.Client)
                .Include(b=>b.Book)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Orders>> GetClientOrders(int id)
        {
            return await Search(o => o.ClientId == id);
        }

    }
}
