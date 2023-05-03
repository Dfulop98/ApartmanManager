

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
        public bool CheckAnyImages() => _db.Images.Any();

        public bool CheckImagesByType(string type) => _db.Images.Any(x => x.Type == type);
        
        public List<Images> GetAllImages()
        {
             return _db.Images.ToList();
        }
        public List<Images> GetImagesByType(string type)
        {
            return _db.Images.Where(i => i.Type == type).ToList();
        }
        public void AddImage(Images image)
        {
            _db.Images.Add(image);
            _db.SaveChanges();
        }

    }
}
