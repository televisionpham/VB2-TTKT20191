using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.DAL.Abstract;

namespace VYT.DAL.Concrete
{
    class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add(T entity)
        {
            var e = _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return e;
        }

        public T Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetPage(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public T Remove(T entity)
        {
            var e = _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
            return e;
        }
    }
}
