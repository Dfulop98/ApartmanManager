namespace DataAccessLayer.Interfaces
{
    public interface IGenericDataAccess<T> where T : class
    {
        bool CheckEntity(int id);
        bool CheckEntities();
        List<T> GetEntities(string navigationProperty = null);
        T GetEntity(int id, string navigationProperty = null);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void RemoveEntity(int id);

    }
}
