using cw8_2.Entities;

namespace cw8_2.Context.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public T Create(T entity);
        public T Update(T entity);
        public bool Delete(string id);
        public List<T> GetAll();
        public T GetByID(string id);
        public void SaveChanges(); 
    }
}
