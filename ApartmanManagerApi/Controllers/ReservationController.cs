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
        public OkObjectResult GetAllReservations()
        {
            return Ok(_reservatonService.GetReservations());
        } 
        
        [HttpGet("/api/reservation/{id}")]
        public OkObjectResult GetReservationById(int id)
        {
            return Ok(_reservatonService.GetReservation(id));
        }

        [HttpPost("/api/reservation/add")]
        public OkObjectResult AddReservation([FromBody] Reservation reservation)
        {
            return Ok(_reservatonService.AddReservation(reservation));
        }

        [HttpPut("/api/reservation/update")]
        public OkObjectResult UpdateReservation([FromBody] Reservation reservation)
        {
            return Ok(_reservatonService.UpdateReservation(reservation));
        }
        
        [HttpDelete("/api/reservation/remove/{id}")]
        public OkObjectResult DeleteRoom(int id)
        {
            return Ok(_reservatonService.RemoveReservation(id));
        }
    }
}
