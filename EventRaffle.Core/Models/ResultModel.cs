namespace EventRaffle.Core.Models
{
    public class ResultModel<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

        public static ResultModel<T> Ok(T data, string? message = null, int? statusCode = 200) =>
            new ResultModel<T> { Success = true, Data = data, Message = message, StatusCode = statusCode };

        public static ResultModel<T> Fail(string message, int? statusCode = 400, List<string>? errors = null) => new ResultModel<T>
        {
            Success = false,
            Message = message,
            StatusCode = statusCode,
            Errors = errors ?? new List<string>(),
            Data = default
        };
    }

}
