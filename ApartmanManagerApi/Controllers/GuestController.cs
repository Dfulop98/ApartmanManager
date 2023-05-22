using DataModelLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterfaces;

namespace ApartmanManagerApi.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        public IActionResult GetGuests()
        {
            var result = _guestService.GetGuests();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public IActionResult GetGuest(int id)
        {
            var result = _guestService.GetGuest(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpPost]
        public IActionResult AddGuest(Guest guest)
        {
            var result = _guestService.AddGuest(guest);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetGuest), new { id = guest.Id }, result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGuest(int id, Guest guest)
        {
            if (id != guest.Id)
            {
                return BadRequest();
            }

            var result = _guestService.UpdateGuest(guest);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGuest(int id)
        {
            var result = _guestService.RemoveGuest(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return NotFound(result.ErrorMessage);
        }
    }
}
