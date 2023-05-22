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

        public ImagesController(IImagesService imagesService)
        {
            _imagesService = imagesService;
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

        [HttpGet("room/{id}")]
        public IActionResult GetImagesByRoomId(int id)
        {
            var result = _imagesService.GetImagesByRoomId(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost]
        public IActionResult AddImage([FromBody] Stream imageStream, [FromQuery] string imageName, [FromQuery] string type)
        {
            var result = _imagesService.AddImage(imageStream, imageName, type);
            if (result.IsSuccess)
            {
                return Created("", result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
        
    }
}
