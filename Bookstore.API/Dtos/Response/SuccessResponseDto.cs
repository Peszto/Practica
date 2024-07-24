namespace Bookstore.API.Dtos.Response
{
    public class SuccessResponseDto : ApiResponse
    {
        public string SuccessMessage { get; set; }
        public SuccessResponseDto() {
            Success= true;
        }

    }
}
