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
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            IEnumerable<Room> allRooms = await _roomService.GetAllRoomAsync();
            return Ok(allRooms);
        }

        [HttpGet("/api/room/{id}")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            return Ok(await _roomService.GetRoomByIdAsync(id));
        }

        [HttpPost("/api/room/add")]
        public async Task<ActionResult> AddRoom([FromBody] Room room)
        {
            return Ok(await _roomService.AddRoomAsync(room));
        }

        [HttpPut("/api/room/update")]
        public async Task<ActionResult> UpdateRoom([FromBody] Room room)
        {
            return Ok(await _roomService.UpdateRoomAsync(room));
        }

        [HttpDelete("/api/room/remove/{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            return Ok(await _roomService.RemoveRoomByIdAsync(id));
        }


    }
}