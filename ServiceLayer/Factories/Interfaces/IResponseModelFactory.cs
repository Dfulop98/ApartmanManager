using DTOLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.Factories.Interfaces
{
    public interface IResponseModelFactory
    {
        ResponseModel CreateResponseModel(string status, string message);
        ResponseModel CreateResponseModel(string status, string message, UniversalDTO model);
        ResponseModel CreateResponseModel(string status, string message, IEnumerable<UniversalDTO> models);
    }
}
