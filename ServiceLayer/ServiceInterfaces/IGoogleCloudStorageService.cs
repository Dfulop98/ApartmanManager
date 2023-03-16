using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IGoogleCloudStorageService
    {
        public string UploadImage(Stream imageStream, string imageName);
    }
}
