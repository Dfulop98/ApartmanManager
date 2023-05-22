using DataModelLayer.Models;
using DTOLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;

namespace ApartmanManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public ActionResult<Result<List<UniversalDTO>>> GetReservations()
        {
            var result = _reservationService.GetReservations();
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<Result<UniversalDTO>> GetReservation(int id)
        {
            var result = _reservationService.GetReservation(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public ActionResult<Result<Reservation>> AddReservation([FromBody] Reservation reservation)
        {
            var result = _reservationService.AddReservation(reservation);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        public ActionResult<Result<Reservation>> UpdateReservation(int id, [FromBody] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            var result = _reservationService.UpdateReservation(reservation);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public ActionResult<Result<Reservation>> RemoveReservation(int id)
        {
            var result = _reservationService.RemoveReservation(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}
