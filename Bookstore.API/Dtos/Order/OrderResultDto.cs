namespace BookStore.API.Dtos.Order
{
    public class OrderResultDto
    {
        public int Id { get; set; } 
        public int OrderNr { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int ClientId { get; set; }
    }
}
