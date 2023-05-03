using System.Net;

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
