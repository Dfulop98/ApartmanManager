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

        } 
        
        [HttpGet("/api/reservation/{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {

        }

        [HttpPost("/api/reservation/add")]
        public async Task<ActionResult> AddReservation()
        {

        }

        [HttpPut("/api/reservation/update")]
        public async Task<ActionResult> UpdateReservation()
        {

        }
        
        [HttpDelete("/api/reservation/remove/{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {

        }
    }
}
