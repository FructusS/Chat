namespace AvaloniaChat.Models.Response;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public ErrorInfoResponse? Error { get; set; }
    public T Data { get; set; }
}