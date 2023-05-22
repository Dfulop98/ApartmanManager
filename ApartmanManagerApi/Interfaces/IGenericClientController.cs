using DTOLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApartmanManagerApi.Interfaces
{
    public interface IGenericClientController
    {
        IActionResult GetAllOperations(string type);
        IActionResult AddOperations(string type, InputDTO entity);
        IActionResult DeleteOperations(string type, int id);
        IActionResult UpdateOperations(string type, int id, dynamic entity);
    }
}
