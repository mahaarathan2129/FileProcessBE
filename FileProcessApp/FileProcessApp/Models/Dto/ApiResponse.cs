using Microsoft.AspNetCore.Http;

namespace FileProcessingApp.Models.Dto
{
    public class ApiResponse<T>
    {
        public ApiResponse(int statusCode, string message, bool isSuccess, T? data)
        {
            StatusCode = statusCode;
            Message = message;
            IsSuccess = isSuccess;
            Data = data;
        }

        public int StatusCode { get; }
        public string Message { get; }
        public bool IsSuccess { get; }
        public T? Data { get; }

        public static ApiResponse<T> Success(T? data = default, string message = "Success", int statusCode = 200)
        {
            return new ApiResponse<T>(statusCode, message, true, data);
        }

        public static ApiResponse<T> Error(string message, int statusCode = 400)
        {
            return new ApiResponse<T>(statusCode, message, false, default);
        }
    }

}
