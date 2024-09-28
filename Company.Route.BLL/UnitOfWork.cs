using Company.Route.BLL.Interfaces;
using Company.Route.BLL.Repositories;
using Company.Route.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDepartmentRepository _departmentRepository;
        private IEmployeeRepository _employeeRepository;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _departmentRepository = new DepartmentRepository(context);
            _employeeRepository = new EmployeeRepository(context);
            _context = context;
        }

        public IDepartmentRepository DepartmentRepository => _departmentRepository;
        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
