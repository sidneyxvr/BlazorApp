using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BlazorApp.Client.Exceptions
{
    public class HttpException : Exception
    {
        public List<string> Errors { get; }
        public HttpStatusCode StatusCode { get; }
        public HttpException(HttpStatusCode statusCode, List<string> errors)
        {
            Errors = errors;
            StatusCode = statusCode;
        }
    }
}
