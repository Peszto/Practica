namespace Bookstore.API.Dtos.Response
{
    public class ErrorResponseDto : ApiResponse
    {
        public string ErrorMessage { get; set; }

        public ErrorResponseDto()
        {
            Success = false;
        }
    }
}
