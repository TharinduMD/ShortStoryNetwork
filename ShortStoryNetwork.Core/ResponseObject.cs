using System;
using System.Collections.Generic;
using System.Text;

namespace ShortStoryNetwork.Core
{
    public class ResponseObject
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public Error Error { get; set; }
    }
    public class Error
    {
        public string Message { get; set; }
    }
}
