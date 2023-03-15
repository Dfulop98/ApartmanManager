using ServiceLayer.Factories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Factories.Interfaces
{
    public interface IResponseModelFactory<T>
    {
        ResponseModel<T> CreateResponseModel(string status, string message);
        ResponseModel<T> CreateResponseModel(string status, string message, T model);
        ResponseModel<T> CreateResponseModel(string status, string message, IEnumerable<T> models);
    }
}
