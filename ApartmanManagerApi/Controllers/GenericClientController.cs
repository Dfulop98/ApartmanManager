using ApartmanManagerApi.Interfaces;
using DataModelLayer.Models;
using DTOLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;

namespace ApartmanManagerApi.Controllers
{
    [Route("api/cs")]
    public class GenericClientController : ControllerBase, IGenericClientController
    {
        private readonly IGuestService _guestService;
        private readonly IRoomService _roomService;
        private readonly IReservationService _reservationService;
        private readonly IImagesService _imagesService;



        public GenericClientController
            (
            IGuestService guestService 
            ,IRoomService roomService
            ,IReservationService reservationService
            ,IImagesService imagesService
            )
        {
            _guestService = guestService;
            _roomService = roomService;
            _reservationService= reservationService;
            _imagesService = imagesService; 
        }


        [HttpGet]
        [Route("{type}")]
        public IActionResult GetAllOperations(string type)
        {
            var services = new Dictionary<string, Func<Result<List<UniversalDTO>>>>
            {
                { "Guest", () => _guestService.GetGuests() },
                { "Reservation", () => _reservationService.GetReservations() },
                { "Room", () => _roomService.GetRooms() },
                { "Images", () => _imagesService.GetAllImages() },
            };

            if (services.TryGetValue(type, out var serviceFunc))
            {
                var result = serviceFunc();
                if (result.IsSuccess)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return NotFound(result.ErrorMessage);
                }
            }

            return BadRequest("Could not find any object of given type");
        }

        [HttpPost]
        [Route("{type}")]
        public IActionResult AddOperations(string type, [FromBody] dynamic entity)
        {
            var services = new Dictionary<string, Func<dynamic, Result<dynamic>>>
            {
                { "Guest", (e) => _guestService.AddGuest(e) },

                { "Reservation", (e) =>{

                    if (e is not Reservation reservation)
                        return Result<dynamic>.Failure("reservation is invalid");


                    UniversalDTO roomDTO = _roomService.GetRoom(reservation.RoomId).Data;

                    if (roomDTO == null || roomDTO.GetProperty<Room>("isAvailable").IsAvailable)
                            return Result<dynamic>.Failure("reserved room is null or not available");
                    
                    return _reservationService.AddReservation(e);
                    }
                },

                { "Room", (e) => _roomService.AddRoom(e) },
            };

            if (services.TryGetValue(type, out var serviceFunc))
            {
                var result = serviceFunc(entity);
                
                if (result.IsSuccess)
                {
                    return Created("", result.Data);
                }
                else
                {
                    return Conflict(result.ErrorMessage);
                }
            }

            return BadRequest("Could not find any object of given type");
        }

        [HttpDelete]
        [Route("{type}/{id}")]
        public IActionResult DeleteOperations(string type, int id)
        {
            var services = new Dictionary<string, Func<dynamic, Result<dynamic>>>
            {
                { "Guest", (i) => _guestService.RemoveGuest(i) },
                { "Reservation", (i) => _reservationService.RemoveReservation(i) },
                { "Room", (i) => _roomService.RemoveRoom(i) },
                // { "Images", (i) => _imagesService.RemoveImage(i) },
            };

            if (services.TryGetValue(type, out var serviceFunc))
            {
                var result = serviceFunc(id);
                if (result.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return NotFound(result.ErrorMessage);
                }
            }

            return BadRequest("Could not find any object of given type");
        }


        [HttpPut]
        [Route("{type}/{id}")]
        public IActionResult UpdateOperations(string type, int id, [FromBody] dynamic entity)
        {
            var services = new Dictionary<string, Func<int, dynamic, Result<dynamic>>>
            {
                { "Guest", (i, e) => _guestService.UpdateGuest(e) },

                { "Reservation", (i, e) => {
                    if (e is not Reservation reservation)
                        return Result<dynamic>.Failure("reservation is invalid");


                    UniversalDTO roomDTO = _roomService.GetRoom(reservation.RoomId).Data;

                    if (roomDTO == null || roomDTO.GetProperty<Room>("isAvailable").IsAvailable)
                            return Result<dynamic>.Failure("reserved room is null or not available");
                    return _reservationService.UpdateReservation(e);
                } },

                { "Room", (i, e) => _roomService.UpdateRoom(e) },
                //{ "Images", (i, e) => _imagesService.UpdateImage(e) },
            };

            if (services.TryGetValue(type, out var serviceFunc))
            {
                var result = serviceFunc(id, entity);
                if (result.IsSuccess)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return NotFound(result.ErrorMessage);
                }
            }

            return BadRequest("Could not find any object of given type");
        }


    }
}
