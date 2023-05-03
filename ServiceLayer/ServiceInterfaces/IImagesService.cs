using DataModelLayer.Models;
using DTOLayer.Models;
using ServiceLayer.Factories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IImagesService
    {
        public Result<List<UniversalDTO>> GetAllImages();
        public Result<List<UniversalDTO>> GetImagesByType(string type);
        public Result<Images> AddImage(Stream imageStream, string imageName, string type);
    }
}
