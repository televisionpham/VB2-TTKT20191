using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.DAL.Abstract
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T Get(int Id);
        IEnumerable<T> GetPage(int pageIndex, int pageSize);
        T Add(T entity);
        T Remove(T entity);
    }
}
