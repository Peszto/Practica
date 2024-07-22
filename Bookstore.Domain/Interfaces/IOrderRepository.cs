using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Orders>
    {
        new Task<List<Orders>> GetAll();
        new Task<Orders> GetById(int id);
        Task<IEnumerable<Orders>> GetClientOrders(int id);
    }
}
