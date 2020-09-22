using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.DAL.Abstract;
using VYT.DAL.Concrete;

namespace VYT.DAL
{
    public class DALFactory
    {
        private static volatile DALFactory _uniqueInstance;
        private static readonly object _lockObject = new object();

        private DALFactory() { }

        public static DALFactory GetInstance()
        {
            if (_uniqueInstance == null)
            {
                lock (_lockObject)
                {
                    if (_uniqueInstance == null)
                    {
                        _uniqueInstance = new DALFactory();
                    }
                }
            }

            return _uniqueInstance;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(new VYT_TTKTEntities());
        }
    }
}
