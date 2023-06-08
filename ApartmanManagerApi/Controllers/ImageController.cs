using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using DTOLayer.Models;
using DataModelLayer.Models;
using System.IO;
using ServiceLayer.ServiceInterfaces;

namespace ApartmanManagerApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService _imagesService;
        private readonly IRoomService _roomService;

        public ImagesController(IImagesService imagesService, IRoomService roomService)
        {
            _imagesService = imagesService;
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult GetAllImages()
        {
            var result = _imagesService.GetAllImages();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("type/{type}")]
        public IActionResult GetImagesByType(string type)
        {
            var result = _imagesService.GetImagesByType(type);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        //[HttpGet("room/{id}")]
        //public IActionResult GetImagesByRoomId(int id)
        //{
        //    var result = _imagesService.GetImagesByRoomId(id);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result.Data);
        //    }
        //    return BadRequest(result.ErrorMessage);
        //}

        [HttpPost]
        public IActionResult AddImage(IFormFile image, string type, int? roomId)
        {
            Stream imageStream = image.OpenReadStream();
            string imageName = image.FileName;

            var result = _imagesService.AddImage(imageStream, imageName, type);
            
            if(type == "room")
            {
                if (roomId != null)
                {
                    //UniversalDTO relatedRoomDTO = _roomService.GetRoom((int)roomId).Data;
                    //Room relatedRoom = new()
                    //{
                    //    Id = relatedRoomDTO.GetProperty<Room>("Id").Id,
                    //    RoomNumber = relatedRoomDTO.GetProperty<Room>("RoomNumber").RoomNumber,
                    //    Capacity = relatedRoomDTO.GetProperty<Room>("Capacity").Capacity,
                    //    IsAvailable = relatedRoomDTO.GetProperty<Room>("IsAvailable").IsAvailable,
                    //    PricePerNight = relatedRoomDTO.GetProperty<Room>("PricePerNight").PricePerNight,
                    //    Description = relatedRoomDTO.GetProperty<Room>("Description").Description,
                    //    Reservations = relatedRoomDTO.GetProperty<Room>("Reservations").Reservations,
                    //    Images = relatedRoomDTO.GetProperty<Room>("Images").Images,
                    //};
                    var linkImageResponse = _roomService.LinkImageToRoom(result.Data, (int)roomId);
                    if (linkImageResponse.IsSuccess)
                    {
                        return Created("Link to room and upload", result.Data);
                    }
                    return BadRequest(linkImageResponse.ErrorMessage);
                }
                return BadRequest("Room id is null while linking photo");
                
            }
            if (result.IsSuccess)
            {    
                    return Created("", result.Data);
            }
            return BadRequest(result.ErrorMessage);
            
            


        }
        
    }
}
