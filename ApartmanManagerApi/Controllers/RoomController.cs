using DataModelLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterfaces;

namespace ApartmanManagerApi.Controllers
{
    public class RoomController : ControllerBase

    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("/api/room")]
        public OkObjectResult GetAllRooms()
        {
            return Ok(_roomService.GetRooms());
        }

        [HttpGet("/api/room/{id}")]
        public OkObjectResult GetRoomById(int id)
        {
            return Ok(_roomService.GetRoom(id));
        }

        [HttpPost("/api/room/add")]
        public OkObjectResult AddRoom([FromBody] Room room)
        {
            return Ok(_roomService.AddRoom(room));
        }

        [HttpPut("/api/room/update")]
        public OkObjectResult UpdateRoom([FromBody] Room room)
        {
            return Ok(_roomService.UpdateRoom(room));
        }

        [HttpDelete("/api/room/remove/{id}")]
        public OkObjectResult DeleteRoom(int id)
        {
            return Ok(_roomService.RemoveRoom(id));
        }


    }
}