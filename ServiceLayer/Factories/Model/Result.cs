using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Factories.Model
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Data = data, IsSuccess = true };
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T> { ErrorMessage = errorMessage, IsSuccess = false };
        }
    }
}
