using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterfaces;
using ServiceLayer.Services;

namespace ApartmanManagerApi.Controllers
{
    public class ImagesController: ControllerBase
    {
        private readonly IImagesService _imagesService;
        public ImagesController(IImagesService imagesService)
        {
            _imagesService = imagesService;
        }
        [HttpGet("/api/images")]
        public OkObjectResult GetAllImages()
        {
            return Ok(_imagesService.GetImages());
        }
        [HttpPost("/api/images/upload-image")]
        public IActionResult UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("The pic doesnt exists.");
            }
            using var imageStream = image.OpenReadStream();

            return Ok(_imagesService.AddImage(imageStream, image.FileName));
        }
    }
}
