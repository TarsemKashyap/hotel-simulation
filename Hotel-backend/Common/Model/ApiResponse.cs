using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public  class ApiResponse
    {
        public string Message { get; private set; }

        public object Data { get; private set; }

        public static ApiResponse CreateResponse(string message)
        {
            return new ApiResponse()
            {
                Message = message,
            };

        }

        public static ApiResponse CreateResponse(string message, object data)
        {
            return new ApiResponse()
            {
                Message = message,
                Data = data

            };

        }
    }
}
