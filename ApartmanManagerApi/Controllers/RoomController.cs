using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using DataModelLayer.Models;
using DTOLayer.Models;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;

namespace ApartmanManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public ActionResult<Result<List<UniversalDTO>>> GetRooms()
        {
            var result = _roomService.GetRooms();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public ActionResult<Result<UniversalDTO>> GetRoom(int id)
        {
            var result = _roomService.GetRoom(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        public ActionResult<Result<Room>> AddRoom([FromBody] Room room)
        {
            var result = _roomService.AddRoom(room);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public ActionResult<Result<Room>> UpdateRoom(int id, [FromBody] Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var result = _roomService.UpdateRoom(room);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public ActionResult<Result<Room>> RemoveRoom(int id)
        {
            var result = _roomService.RemoveRoom(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("LinkImage/{id}")]
        public ActionResult<Result<Room>> LinkImageToRoom([FromBody] Images images, int id)
        {
            var result = _roomService.LinkImageToRoom(images, id);
            if (result.IsSuccess)
            {
               return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }

}