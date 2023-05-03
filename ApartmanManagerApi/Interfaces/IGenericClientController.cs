using Microsoft.AspNetCore.Mvc;

namespace ApartmanManagerApi.Interfaces
{
    public interface IGenericClientController
    {
        IActionResult GetAllOperations(string type);
        IActionResult AddOperations(string type, dynamic entity);
        IActionResult DeleteOperations(string type, int id);
        IActionResult UpdateOperations(string type, int id, dynamic entity);
    }
}
