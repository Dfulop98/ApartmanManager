using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Common;
using ServiceLayer.Factories.Model;
using ServiceLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.ServiceInterfaces;
using DTOLayer.Configurations;
using DTOLayer.Factories;
using DTOLayer.Models;

namespace ServiceLayer.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IResponseModelFactory _responseModel;
        private readonly IImagesDataAccess _context;
        public ImagesService(
            IResponseModelFactory responseModel,
            IImagesDataAccess context
            )
        {
            _responseModel = responseModel;
            _context = context;
        }

        public ResponseModel GetImages()
        {
            bool ImagesExists = _context.CheckImages();
            if (ImagesExists)
            {
                List<OutSideImage> images = _context.GetImages();
                List<UniversalDTO> imagesDTOs = UniversalDtoFactory.CreateListFromObjects(
                    images,
                    DTOConfig.ImagesProperties);

                return _responseModel.CreateResponseModel(Status.Ok, Messages.RoomsGetOk, imagesDTOs);
            }
            else
            {
                return _responseModel.CreateResponseModel(Status.NotFound, Messages.RoomNotFound);
            }
        }
        public ResponseModel AddImage(Stream imageStream, string imageName)
        {
            
            string ImageUrl = GoogleCloudStorageService.UploadImage(imageStream, imageName);
            OutSideImage image = new()
            {
                Url = ImageUrl
            };
            _context.AddImage(image);
            return _responseModel.CreateResponseModel(Status.Created, Messages.ImagesCreated);

            
        }
    }
}
