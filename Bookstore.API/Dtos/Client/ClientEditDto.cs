using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Dtos.Client
{
    public class ClientEditDto
    {
        private const string _errorMessage = "The field {0} is required";
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage =_errorMessage)]
        public string FirstName { get; set; }

        [Required(ErrorMessage =_errorMessage)]
        public string LastName { get; set; }

        [Required(ErrorMessage =_errorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage =_errorMessage)]
        public string PhoneNr { get; set; }

        [Required(ErrorMessage =_errorMessage)]
        public string Address { get; set; }
    }
}
