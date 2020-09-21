using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYT.DAL.Abstract;

namespace VYT.DAL.Concrete
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IJobRepository JobRepository => new JobRepository(_dbContext);

        public IJobLogRepository JobLogRepository => new JobLogRepository(_dbContext);
    }
}
