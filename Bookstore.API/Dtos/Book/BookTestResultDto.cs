using BookStore.Domain.Models;

namespace BookStore.API.Dtos.Book
{
    public class BookTestResultDto
    {
        public int Id { get; set; }
        public BasicModel CategoryId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Pieces { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
