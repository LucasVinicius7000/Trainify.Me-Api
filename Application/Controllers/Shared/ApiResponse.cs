namespace Trainify.Me_Api.Application.Controllers.Shared
{
    public class ApiResponse<T> where T : class?
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ClientMessage { get; set; } = string.Empty;
        public string TechnicalMessage { get; set; } = string.Empty;
        public List<dynamic> Errors { get; set; } = new List<dynamic>();

        private ApiResponse() { }

        public static ApiResponse<T> SucessResponse(T Data, string ClientMessage)
        {
            return new ApiResponse<T>
            {
                Data = Data,
                ClientMessage = ClientMessage,
                IsSuccess = true,
            };
        }

        public ApiResponse<T> FailureResponse(string ClientMessage, string TechnicalMessage, List<dynamic> Errors)
        {
            return new ApiResponse<T>
            {
                Data = null,
                ClientMessage = ClientMessage,
                TechnicalMessage = TechnicalMessage,
                Errors = Errors,
                IsSuccess = false,
            };
        }
    }
}