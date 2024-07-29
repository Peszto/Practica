using System;
using System.Collections.Concurrent;
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
            if (!IsOrderQuantityPositiveNumber(order.Quantity)){
                throw new Exception("Quantity has to be greater than 0 !");
            }
            if (!IsOrderQuantityValid(book.Pieces,order.Quantity, int.MaxValue))
            {
                throw new Exception("There are not enough pieces of books !");
            }
            
            book.Pieces -= order.Quantity;

            order.TotalPrice = order.Quantity * book.Price;
            order.OrderNr = OrderNumberGenerator();

            await _orderRepository.Add(order);
            await _bookRepository.Update(book);
            return order;
        }

        private bool IsOrderQuantityPositiveNumber(int requestedQuantity)
        {
            return requestedQuantity > 0;
        }
        private bool IsOrderQuantityValid(int pieces, int requestedQuantity, int oldQuantity)
        {
            return pieces >= requestedQuantity || requestedQuantity<=oldQuantity;
        }

        private bool IsOrderExisting(Orders order)
        {
            return _orderRepository.Search(o => o.OrderNr == order.OrderNr
            && o.ClientId == order.ClientId
            && o.BookId == order.BookId
            && o.Id == order.Id
            ).Result.Any();
        }
        public async Task<Orders> Update(Orders order)
        {
            Book book = await _bookRepository.GetById(order.BookId);
            Orders oldOrder = await _orderRepository.GetById(order.Id);

            if (!IsOrderQuantityPositiveNumber(order.Quantity))
            {
                throw new Exception("Quantity has to be greater than 0 !");
            }
            if (!IsOrderQuantityValid(book.Pieces, order.Quantity, oldOrder.Quantity))
            {
                throw new Exception("There are not enough pieces of books !");
            }
            if(!IsOrderExisting(order))
            {
                throw new Exception("The order you are trying to edit does not exist!");
            }                

            

            if (oldOrder.Quantity > order.Quantity)
            {
                book.Pieces += oldOrder.Quantity - order.Quantity;
            }
            else if (book.Pieces - oldOrder.Quantity + order.Quantity >=0)
            {
                book.Pieces -= (order.Quantity - oldOrder.Quantity);
            }


            order.TotalPrice = order.Quantity * book.Price;
            await _orderRepository.Update(order);
            await _bookRepository.Update(book);
            return order;
        }

        public async Task<bool> Remove(Orders order)
        {
            order.Book.Pieces += order.Quantity;
            await _bookRepository.Update(order.Book);
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

        private int OrderNumberGenerator()
        {
            Random random = new Random();
            int orderNumber = random.Next(1000000, 9999999); 
            return orderNumber;
        }
    }
}
