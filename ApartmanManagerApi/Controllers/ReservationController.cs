using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterfaces;
using DataModelLayer.Models;
using System.Collections.Generic;

namespace ApartmanManagerApi.Controllers
{
    public class ReservationController : ControllerBase
    {
        private readonly IReservatonService _reservatonService;
        public ReservationController(IReservatonService reservatonService) 
        {
            _reservatonService = reservatonService;
        }

        [HttpGet("/api/reservations")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllReservations()
        {
            IEnumerable<Reservation> allReservation = await _reservatonService.GetReservationsAsync();
            return Ok(allReservation);
        } 
        
        [HttpGet("/api/reservation/{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {
            return Ok(await _reservatonService.GetReservationByIdAsync(id));
        }

        [HttpPost("/api/reservation/add")]
        public async Task<ActionResult> AddReservation([FromBody] Reservation reservation)
        {
            return Ok(await _reservatonService.AddReservationAsync(reservation));
        }

        [HttpPut("/api/reservation/update")]
        public async Task<ActionResult> UpdateReservation([FromBody] Reservation reservation)
        {
            return Ok(await _reservatonService.UpdateReservationAsync(reservation));
        }
        
        [HttpDelete("/api/reservation/remove/{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            return Ok(await _reservatonService.RemoveReservationAsync(id));
        }
    }
}
