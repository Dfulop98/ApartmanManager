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
        public ResponseModel GetImages();
        public ResponseModel AddImage(Stream imageStream, string imageName);
    }
}
