using DataModelLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterfaces;

namespace ApartmanManagerApi.Controllers
{
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;
        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet("/api/guests")]
        public OkObjectResult GetGuests()
        {
            var allGuest = _guestService.GetGuests();
            return Ok(allGuest);
        }

        [HttpGet("/api/guest/{id}")]
        public OkObjectResult GetGuest(int id)
        {
            return Ok(_guestService.GetGuest(id));
        }

        [HttpPost("/api/guest/add")]
        public OkObjectResult AddGuest([FromBody] Guest guest)
        {
            return Ok(_guestService.AddGuest(guest));
        }

        [HttpPut("/api/guest/update")]
        public OkObjectResult Updateguest([FromBody] Guest guest)
        {
            return Ok(_guestService.UpdateGuest(guest));
        }

        [HttpDelete("/api/guest/remove/{id}")]
        public OkObjectResult DeleteGuest(int id)
        {
            return Ok(_guestService.RemoveGuest(id));
        }
    }
}
