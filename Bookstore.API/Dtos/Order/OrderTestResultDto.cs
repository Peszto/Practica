using BookStore.Domain.Models;

namespace BookStore.API.Dtos.Order
{
    public class OrderTestResultDto
    {
        public int Id { get; set; }
        public int OrderNr { get; set; }
        public BasicModel BookId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public BasicModel ClientId { get; set; }
    }
}
