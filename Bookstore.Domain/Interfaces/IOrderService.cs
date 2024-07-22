using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IOrderService : IDisposable
    {
        Task<IEnumerable<Orders>> GetAll();
        Task<Orders> GetById(int id);
        Task<Orders> Add(Orders order);
        Task<Orders> Update(Orders order);
        Task<bool> Remove(Orders order);
        Task<IEnumerable<Orders>> GetClientOrders(int id);
    }
}
