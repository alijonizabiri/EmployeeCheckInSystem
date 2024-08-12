using System.Net;

namespace Domain.Core.ApiResponses;

public class Response<T>
{
    public T Data { get; set; }
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; } = [];

    public Response(T data)
    {
        StatusCode = 200;
        Errors = [];
        Data = data;
    }

    public Response(HttpStatusCode statusCode, string message)
    {
        StatusCode = (int)statusCode;
        Errors.Add(message);
    }

    public Response()
    {
        
    }
}
