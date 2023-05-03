

using DataAccessLayer.DbAccess;
using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Operations
{
    public class ImagesDataAccess : IImagesDataAccess
    {
        private readonly AMDbContext _db;
        public ImagesDataAccess(AMDbContext db)
        {
            _db= db;
        }
        public bool CheckImages() => _db.OutSideImages.Any();
        public List<OutSideImage> GetImages()
        {
             return _db.OutSideImages.ToList();
        }
        public void AddImage(OutSideImage image)
        {
            _db.OutSideImages.Add(image);
            _db.SaveChanges();

        }
    }
}
