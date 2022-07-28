
namespace productMgtApi.Domain.Services
{
    public class Response<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T? Data {get; private set;}

        public Response(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

    }
}