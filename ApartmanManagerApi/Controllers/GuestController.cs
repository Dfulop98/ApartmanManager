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
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllGuest()
        {
            IEnumerable<Guest> allGuest = await _guestService.GetAllGuestsAsync();
            return Ok(allGuest);
        }

        [HttpGet("/api/guest/{id}")]
        public async Task<ActionResult<Guest>> GetGuestById(int id)
        {
            return Ok(await _guestService.GetGuestByIdAsync(id));
        }

        [HttpPost("/api/guest/add")]
        public async Task<ActionResult> AddGuest([FromBody] Guest guest)
        {
            return Ok(await _guestService.AddGuestAsync(guest));
        }

        [HttpPut("/api/guest/update")]
        public async Task<ActionResult> Updateguest([FromBody] Guest guest)
        {
            return Ok(await _guestService.UpdateGuestAsync(guest));
        }

        [HttpDelete("/api/guest/remove/{id}")]
        public async Task<ActionResult> DeleteGuest(int id)
        {
            return Ok(await _guestService.RemoveGuestByIdAsync(id));
        }
    }
}
