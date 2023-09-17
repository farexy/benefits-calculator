namespace Api.Models;

/// <summary>
/// This model is needed to return error results.
/// </summary>
public class BaseApiResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}