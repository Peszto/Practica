using AutoMapper;
using BookStore.API.Dtos.Order;
using BookStore.Domain.Services;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.Interfaces;
using BookStore.API.Dtos;
using Bookstore.API.Dtos.Response;

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
            var orders = await _orderService.GetAll();
            Console.WriteLine(orders);
            return Ok(_mapper.Map<IEnumerable<OrderResultDto>>(orders));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderResultDto>(order));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(OrderAddDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(new ErrorResponseDto { ErrorMessage = "Model is not valid ! " });

                var order = _mapper.Map<Orders>(orderDto);
                var orderResult = await _orderService.Add(order);

                return Ok(new SuccessResponseDto { SuccessMessage = "Order placed successfully!"});
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseDto {  ErrorMessage = ex.Message});
            }

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, OrderEditDto orderDto)
        {
            try
            {
                if (id != orderDto.Id) return BadRequest(new ErrorResponseDto { ErrorMessage = "The order cannot be edited !" });

                if (!ModelState.IsValid) return BadRequest(new ErrorResponseDto { ErrorMessage = "Model is not valid ! " });

                await _orderService.Update(_mapper.Map<Orders>(orderDto));

                return Ok(new SuccessResponseDto { SuccessMessage = "Order updated successfully !"});
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseDto
                {
                    ErrorMessage = ex.Message
                });
            }
            
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null) return NotFound();

            var result = await _orderService.Remove(order);

            if (!result) return BadRequest();

            return Ok();
        }

        [HttpGet]
        [Route("/my_orders/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClientOrders(int id)
        {
            var result = await _orderService.GetClientOrders(id);
            if (result == null) return NotFound();
            return Ok(_mapper.Map<OrderResultDto>(result));
        }

    }
}
