using cw8_2.Context.Interfaces;
using cw8_2.Entities;
using cw8_2.Exceptions;

namespace cw8_2.Context
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public Serialization<T> _serialization;

        string _filePath;

        List<T> _items;

        public GenericRepository(string filePath)
        {
            _filePath = filePath;
            _serialization = new Serialization<T>();
            _items = _serialization.DeserializeFromJsonFile(filePath);
        }

        public T Create(T entity)
        {
            try
            {
                _items.Add(entity);
                SaveChanges();
                return entity;
            }
            catch
            {
                throw new NotSuccessfullError();
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var targetItem = GetByID(id);
                targetItem.IsDeleted = true;

                SaveChanges();
                return true;
            }
            catch
            {
                throw new NotSuccessfullError();
            }
        }

        public List<T> GetAll()
        {
            return _items;
        }

        public void SaveChanges()
        {
            _serialization.SerializeToJsonFile(_filePath, _items);
        }

        public T Update(T entity)
        {
            try
            {
                var newItem = _items.FirstOrDefault(p => p.Id == entity.Id);
                newItem.Id = "0";
                SaveChanges();

                return newItem;
            }
            catch
            {

                throw new NotSuccessfullError();
            }

        }

        public T GetByID(string id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }
    }
}
