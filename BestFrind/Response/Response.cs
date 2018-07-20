using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestFrind.Response
{
    public class Response<T>
    {
        public T Result { get; set; }

        public int StatusCode { get; set; }
        public string message { get; set; }
    }
}