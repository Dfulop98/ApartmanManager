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
        public bool CheckImages();
        public List<OutSideImage> GetImages();
        public void AddImage(OutSideImage image);
    }
}
