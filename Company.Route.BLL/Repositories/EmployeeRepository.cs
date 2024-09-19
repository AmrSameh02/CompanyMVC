using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Data.Contexts;
using Company.Route.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> ,IEmployeeRepository
    {

        public EmployeeRepository(AppDbContext context):base(context) 
        {
        }

        public IEnumerable<Employee> GetByName(string name)
        {
            return _context.Employees.Where(e => e.Name.ToLower().Contains(name.ToLower())).Include(e => e.WorkFor).ToList();      
        }
    }
}
