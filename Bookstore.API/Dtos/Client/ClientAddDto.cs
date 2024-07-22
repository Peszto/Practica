using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Client
{
    public class ClientAddDto
    {
        private const string _errorMessage = "The field {0} is required";

        [Required(ErrorMessage = _errorMessage)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public string LastName { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [StringLength(100, ErrorMessage = "The field {0} must be a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [StringLength(10,ErrorMessage = "The field {0} must be {1} long.")]
        public string PhoneNr { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public string Address { get; set; }
    }
}
