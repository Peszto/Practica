using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Order
{
    public class OrderAddDto
    {
        private const string _errorMessage = "The field {0} is required";

        [Required(ErrorMessage = _errorMessage)]
        public int ClientId { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public int BookId { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public int OrderNr { get; set; }
        public int TotalPrice { get; set; }
    }
}
