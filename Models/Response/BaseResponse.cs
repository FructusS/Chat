namespace AvaloniaChat.Models.Response;

public class BaseResponse
{
    public bool Success { get; set; }
    public ErrorInfoResponse? Error { get; set; }
    public object? Data { get; set; }
}