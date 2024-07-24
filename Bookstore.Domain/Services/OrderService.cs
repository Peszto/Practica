using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;

        public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository)
        {
            _orderRepository = orderRepository;
            _bookRepository=bookRepository;
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
            Book book = await _bookRepository.GetById(order.BookId);
            if (book.Pieces < order.Quantity){
                return null;
            }
            book.Pieces -= order.Quantity;

            order.TotalPrice = order.Quantity * book.Price;
            await _orderRepository.Add(order);
            await _bookRepository.Update(book);
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

            Book book = await _bookRepository.GetById(order.BookId);
            if (book.Pieces < order.Quantity)
            {
                return null;
            }

            book.Pieces -= order.Quantity;

            order.TotalPrice = order.Quantity * book.Price;
            await _orderRepository.Update(order);
            await _bookRepository.Update(book);
            return order;
        }
        public async Task<bool> Remove(Orders order)
        {
            await _orderRepository.Remove(order);
            return true;
        }

        public async Task<IEnumerable<Orders>> GetClientOrders(int id)
        {
            return await _orderRepository.GetClientOrders(id);
        }

        public void Dispose()
        {
            _orderRepository?.Dispose();
        }
    }
}
