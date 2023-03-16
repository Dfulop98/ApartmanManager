using DTOLayer.Models;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.Factories
{
    public class ResponseModelFactory : IResponseModelFactory
    {
        public ResponseModel CreateResponseModel(string status, string message)
        {
            return new ResponseModel
            {
                Status = status,
                Message = message,
                TimeStamp = DateTime.Now,
            };
        }
        public ResponseModel CreateResponseModel(string status, string message, UniversalDTO model)
        {
            return new ResponseModel
            {
                Status = status,
                Message = message,
                TimeStamp = DateTime.Now,
                Model = model
            };
        }
        public ResponseModel CreateResponseModel(string status, string message, IEnumerable<UniversalDTO> models)
        {
            return new ResponseModel
            {
                Status = status,
                Message = message,
                TimeStamp = DateTime.Now,
                Models = models
            };
        }
    }
}
