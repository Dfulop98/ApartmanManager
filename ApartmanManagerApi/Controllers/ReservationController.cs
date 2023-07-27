
using ApartmanManagerApi.Util;
using DataModelLayer.Models;
using DTOLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;
using ServiceLayer.Services;

namespace ApartmanManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IEmailService _emailService;
        private readonly IOptions<StmpConfig> _stmpConfiguration;
        
        public ReservationController(IReservationService reservationService, IEmailService emailService,IOptions<StmpConfig> configuration)
        {
            _reservationService = reservationService;
            _emailService = emailService;
            _stmpConfiguration = configuration;
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
        
        [HttpGet("dates/{roomId}")]
        public ActionResult<Result<UniversalDTO>> GetReservationDatesByRoomId(int roomId)
        {
            var result = _reservationService.GetReservationsDatesByRoomId(roomId);
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

        [HttpPost("request")]
        public ActionResult<Result<Reservation>> SendRequestEmail(Reservation incomingReservation)
        {
            var stmp = _stmpConfiguration.Value;
            Result<Reservation> result = _emailService.SendRequestEmail 
            (
            incomingReservation,
            stmp.Sender,
            stmp.Receipt,
            stmp.Server,
            stmp.Port,
            stmp.Username,
            stmp.Password
                
                );
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        } 
    }
}
