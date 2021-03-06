﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.DAL.Abstract
{
    public interface IUnitOfWork
    {
        IJobRepository JobRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
