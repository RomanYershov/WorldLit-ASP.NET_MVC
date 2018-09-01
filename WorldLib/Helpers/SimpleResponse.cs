using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldLib.Helpers
{
    public class SimpleResponse
    {
        public bool IsSuccess { get; set; }
        private string ErrorText { get; set; }

        public static SimpleResponse Success()
        {
            return new SimpleResponse { IsSuccess = true };
        }

        public static SimpleResponse<T> Success<T>(T data)
        {
            return new SimpleResponse<T> { IsSuccess = true, Data = data };
        }

        public static SimpleResponse Error(string errorText)
        {
            return new SimpleResponse { ErrorText = errorText };
        }
        public static SimpleResponse<T> Error<T>(string errorText, T data)
        {
            return new SimpleResponse<T> { ErrorText = errorText, Data = data };
        }

    }

    public class SimpleResponse<T> : SimpleResponse
    {
        public T Data { get; set; }
    }
}