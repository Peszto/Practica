using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Domain.Interfaces;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace Bookstore.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<Orders>> GetAll()
        {
            return await _orderRepository.GetAll();
        }
        public async Task<Orders> GetById(int id)
        {
            return await _orderRepository.GetById(id);
        }
        public async Task<Orders> Add(Orders order)
        {
            await _orderRepository.Add(order);
            return order;
        }
        public async Task<Orders> Update(Orders order)
        {
            if (_orderRepository.Search(o => o.OrderNr == order.OrderNr
            && o.TotalPrice == order.TotalPrice
            && o.Quantity == order.Quantity
            && o.ClientId == order.ClientId
            && o.BookId == order.BookId
            && o.OrderNr ==order.OrderNr
            && o.Id != order.Id
            ).Result.Any())
                return null;

            await _orderRepository.Update(order);
            return order;
        }
        public async Task<bool> Remove(Orders order)
        {
            await _orderRepository.Remove(order);
            return true;
        }

        public void Dispose()
        {
            _orderRepository?.Dispose();
        }
    }
}
