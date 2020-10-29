using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.Models;

namespace VYT.DAL.Abstract
{
    public interface IRepository<T> where T: class
    {
        int GetTotal();
        T Get(int id);
        T Add(T entity);
        void Remove(int id);
        void Update(T entity);
    }
}
