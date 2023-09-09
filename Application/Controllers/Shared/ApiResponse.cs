namespace Trainify.Me_Api.Application.Controllers.Shared
{
    public class ApiResponse<T> where T : class?
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ClientMessage { get; set; } = string.Empty;
        public List<dynamic> Errors { get; set; } = new List<dynamic>();

        private ApiResponse() { }

        public static ApiResponse<T> SuccessResponse(T Data, string ClientMessage)
        {
            return new ApiResponse<T>
            {
                Data = Data,
                ClientMessage = ClientMessage,
                IsSuccess = true,
            };
        }

        public static ApiResponse<T> FailureResponse(string ClientMessage, List<dynamic> Errors)
        {
            return new ApiResponse<T>
            {
                Data = null,
                ClientMessage = ClientMessage,
                Errors = Errors,
                IsSuccess = false,
            };
        }
    }
}