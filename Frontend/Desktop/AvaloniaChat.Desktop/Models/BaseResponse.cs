using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaChat.Desktop.Models
{
    public class BaseResponse<T>
    {
        public string ErrorText { get; set; }
        public bool IsError { get; set; }

        public T Data { get; set; }
        public BaseResponse(string errorText, bool isError, T data)
        {
            ErrorText = errorText;
            IsError = isError;
            Data = data;
        }
    }

}
