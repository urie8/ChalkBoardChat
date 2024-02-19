using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.Data.Database
{
    public class GenericAppRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericAppRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }
        public void Delete(int id)
        {
            T? entityToDelete = GetById(id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
            }
        }
        public void DeleteCompositeKey(int firstId, int secondId)
        {
            T? entityToDelete = _dbSet.Find(firstId, secondId);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
            }
        }
    }
}
