using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.DAL.Abstract;
using VYT.Models;

namespace VYT.DAL.Concrete
{
    class Repository<T> : IRepository<T> where T : class
    {
        protected VYT_TTKTEntities _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext as VYT_TTKTEntities;
        }

        public virtual T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(int id)
        {
            throw new NotImplementedException();
        }

        public virtual int GetTotal()
        {
            return _dbContext.Set<T>().Count();
        }

        public virtual void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
