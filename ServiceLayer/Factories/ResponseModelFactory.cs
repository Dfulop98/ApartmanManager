using ServiceLayer.Factories.Interfaces;
using ServiceLayer.Factories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Factories
{
    public class ResponseModelFactory<T> : IResponseModelFactory<T>
    {
        public ResponseModel<T> CreateResponseModel(string status, string message)
        {
            return new ResponseModel<T>
            {
                Status = status,
                Message = message,
                TimeStamp = DateTime.Now,
            };
        }
        public ResponseModel<T> CreateResponseModel(string status, string message, T model)
        {
            return new ResponseModel<T>
            {
                Status = status,
                Message = message,
                TimeStamp = DateTime.Now,
                Model = model
            };
        }
        public ResponseModel<T> CreateResponseModel(string status, string message, List<T> models)
        {
            return new ResponseModel<T>
            {
                Status = status,
                Message = message,
                TimeStamp = DateTime.Now,
                Models = models
            };
        }
    }
}
