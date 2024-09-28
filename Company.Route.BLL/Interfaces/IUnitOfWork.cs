﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get;  }
        public IEmployeeRepository EmployeeRepository { get; }
        Task<int> CompleteAsync();
    }
}
