using DataModelLayer.Models;
using ServiceLayer.Factories.Model;
using ServiceLayer.Factories;
using DataAccessLayer.Interfaces;
using ServiceLayer.ServiceInterfaces;
using DTOLayer.Configurations;
using DTOLayer.Factories;
using DTOLayer.Models;

namespace ServiceLayer.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IImagesDataAccess _context;
        public ImagesService(
            IImagesDataAccess context
            )
        {
            _context = context;
        }

        public Result<List<UniversalDTO>> GetAllImages()
        {
            try
            {
                List<Images> images = _context.GetAllImages();
                List<UniversalDTO> imagesDTOs = UniversalDtoFactory.CreateListFromObjects(
                    images,
                    DTOConfig.ImagesProperties);
                if (imagesDTOs.Count == 0)
                    return Result<List<UniversalDTO>>.Failure("there is not images in database");

                return Result<List<UniversalDTO>>.Success(imagesDTOs);

            }
            catch (Exception ex)
            {
                return Result<List<UniversalDTO>>.Failure($"error during get images: {ex.Message}");
            }

           
        }
        public Result<List<UniversalDTO>> GetImagesByType(string type)
        {
            if (string.IsNullOrEmpty(type))
                return Result<List<UniversalDTO>>.Failure("type param null or empty!");

            try
            {
                List<Images> images = _context.GetImagesByType(type);
                List<UniversalDTO> imagesDTOs = UniversalDtoFactory.CreateListFromObjects(
                    images,
                    DTOConfig.ImagesProperties);

                return Result<List<UniversalDTO>>.Success(imagesDTOs);
            }
            catch (Exception ex)
            { 
                return Result<List<UniversalDTO>>.Failure($"error during get images by type {ex.Message}");
            }
        }

        //public Result<List<UniversalDTO>> GetImagesByRoomId(int id)
        //{
        //    if(id < 0)
        //        Result<List<UniversalDTO>>.Failure("incorrect id param");

        //    try
        //    {
        //        List<Images> images = _context.GetImagesByType("Room");
        //        List<Images> roomImages = images.Where(i => i.Room.Id == id).ToList();
                
        //        List<UniversalDTO> roomImagesDTOs = UniversalDtoFactory.CreateListFromObjects(
        //            roomImages,
        //            DTOConfig.ImagesProperties);

        //        if (roomImagesDTOs.Count() == 0)
        //            return Result<List<UniversalDTO>>.Failure("there is no Image with this id!"); 

        //        return Result<List<UniversalDTO>>.Success(roomImagesDTOs);

        //    }catch(Exception ex) 
        //    { 
        //        return Result<List<UniversalDTO>>.Failure($"error during get images by room id {ex.Message}");
        //    }
        //}

        public Result<Images> AddImage(Stream imageStream, string imageName, string type)
        {
            if(imageStream == null || imageStream.Length == 0)
                return Result<Images>.Failure("imageStream param null or empty!");
            
            if (string.IsNullOrEmpty(imageName))
                return Result<Images>.Failure("imageName param null or empty!");

            if (string.IsNullOrEmpty(type))
                return Result<Images>.Failure("type param null or empty!");
            

            string imageUrl = GoogleCloudStorageService.UploadImage(imageStream, imageName);

            if (string.IsNullOrEmpty(imageUrl))
                return Result<Images>.Failure("image url is empty or null after generate");

            Images image = new()
            {
                Url = imageUrl,
                Type = type
            };

            try
            {
                _context.AddImage(image);
            }
            catch(Exception ex)
            {
                return Result<Images>.Failure($"error during upload image {ex.Message}");
            }

            return Result<Images>.Success(image);
            
        }
    }
}
