using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Factories
{
    public class HttpResponseMessageFactory
    {
        public static HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode statusCode, string message)
        {
            HttpResponseMessage response = new(statusCode)
            {
                Content = new StringContent(message)
            };
            return response;
        }
    }
}
