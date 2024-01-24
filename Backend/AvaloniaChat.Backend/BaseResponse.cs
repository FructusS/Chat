namespace AvaloniaChat.Backend
{
    public class Response<T> : BaseResponse
    {
        public T Content { get; set; }

        public Response(T content)
        {
            Content = content;
        }
    }

    public class BaseResponse
    {
        public ResponseType Type { get; set; }
        public BaseResponse()
        {
            Type = ResponseType.Success;
        }
    }

    public enum ResponseType
    {
        Success = 1,
        ErrorMessage = 2,
    }
}
