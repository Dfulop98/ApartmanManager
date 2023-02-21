using DataAccessLayer.DbAccess;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ServiceInterfaces;

namespace ApartmanManagerApi.Controllers
{
    public  class RoomController : ControllerBase

    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("/api/room")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            return Ok(await _roomService.GetAllRoomAsync());
        }


    }
}