namespace Api.Models;

public class ApiResponse<T> : BaseApiResponse
{
    public T? Data { get; set; }
}
