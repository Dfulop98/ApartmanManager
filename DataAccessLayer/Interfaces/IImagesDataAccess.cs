using DataModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IImagesDataAccess
    {
        public bool CheckAnyImages();
        public bool CheckImagesByType(string type);
        public List<Images> GetAllImages();
        public List<Images> GetImagesByType(string type);
        public void AddImage(Images image);
    }
}
