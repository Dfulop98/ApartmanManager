using DataAccessLayer.DbAccess;
using DataAccessLayer.Interfaces;
using DataModelLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Operations
{
    public class GenericDataAccess<T> : IGenericDataAccess<T> where T : class, IEntity
    {
        private readonly AMDbContext _db;

        public GenericDataAccess(AMDbContext db)
        {
            _db = db;
        }

        public bool CheckEntity(int id) => _db.Set<T>().Any(e => e.Id == id);
        public bool CheckEntities() => _db.Set<T>().Any();
        public List<T> GetEntities() => _db.Set<T>().ToList();

        public T GetEntity(int id) => _db.Set<T>().Where(e => e.Id == id).First();
        public void AddEntity(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }
        public void UpdateEntity(T entity)
        {
            try
            {
                _db.Update(entity);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error updating entity", ex);
            }
        }

        public void RemoveEntity(int id)
        {
            try
            {
                T entity = GetEntity(id);
                _db.Remove(entity);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error removing entity", ex);
            }
        }


    }
}
