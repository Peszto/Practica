using AutoMapper;
using BookStore.API.Dtos.Order;
using BookStore.Domain.Services;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.Interfaces;
using BookStore.API.Dtos;
using BookStore.API.Dtos.Response;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        public OrdersController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _orderService.GetAll();
                var result = new List<OrderTestResultDto>();
                foreach (var order in orders)
                {
                    var partialResult = ConvertDataToOrderTestResult(order);
                    result.Add(partialResult);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse {  Message = ex.Message , Success = false});
            }
           
        }

        private OrderTestResultDto ConvertDataToOrderTestResult(Orders dbOrder)
        {
            var result = new OrderTestResultDto();
            result.Id = dbOrder.Id;
            result.BookId = new BasicModel()
            {
                Id = dbOrder.Book.Id,
                Name  = dbOrder.Book.Name
            };
            result.ClientId = new BasicModel()
            {
                Id = dbOrder.Client.Id,
                Name = dbOrder.Client.FirstName + " " + dbOrder.Client.LastName
            };
            result.OrderNr = dbOrder.OrderNr;
            result.Quantity = dbOrder.Quantity;
            result.TotalPrice = dbOrder.TotalPrice;

            return result;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var dbOrder = await _orderService.GetById(id);
                var result = ConvertDataToOrderTestResult(dbOrder);
                return Ok(result);
                //return Ok(_mapper.Map<IEnumerable<OrderResultDto>>(dbOrder));
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse { Message = ex.Message, Success = false });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(OrderAddDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(new ApiResponse { Message = "Model is not valid ! ", Success = false });

                var order = _mapper.Map<Orders>(orderDto);
                var orderResult = await _orderService.Add(order);

                return Ok(new ApiResponse { Message = "Order placed successfully!", Success= true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { Message = ex.Message, Success = false });
            }

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, OrderEditDto orderDto)
        {
            try
            {
                if (id != orderDto.Id) return BadRequest(new ApiResponse { Message = "The order cannot be edited !", Success = false });

                if (!ModelState.IsValid) return BadRequest(new ApiResponse { Message = "Model is not valid ! ", Success = false });

                await _orderService.Update(_mapper.Map<Orders>(orderDto));

                return Ok(new ApiResponse { Message = "Order updated successfully !", Success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Message = ex.Message,
                    Success = false
                });
            }

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var order = await _orderService.GetById(id);
                if (order == null) return NotFound(new ApiResponse { Message = "The order with the specified id does not exist!", Success = false });

                var result = await _orderService.Remove(order);

                if (!result) return BadRequest(new ApiResponse { Message = "An error occured while deleting the order .", Success = false });

                return Ok(new ApiResponse { Message = "Order deleted successfully !", Success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { Message = ex.Message, Success = false });
            }
        }

        [HttpGet]
        [Route("/my_orders/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClientOrders(int id)
        {
            try
            {
                var result = await _orderService.GetClientOrders(id);
                return Ok(_mapper.Map<OrderResultDto>(result));
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse { Message = ex.Message, Success = false });
            }
        }

    }
}
