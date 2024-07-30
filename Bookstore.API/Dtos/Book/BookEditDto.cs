using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Book
{
    public class BookEditDto
    {
        private const string _errorMessage = "The field {0} is required";
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = _errorMessage)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Author { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public int Pieces { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime PublishDate { get; set; }
    }
}