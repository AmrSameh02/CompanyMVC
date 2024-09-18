using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Data.Contexts;
using Company.Route.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Department entity)
        {
            _context.Departments.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _context.Departments.Remove(entity);
            return _context.SaveChanges();
        }

        public Department Get(int? id)
        {
            return _context.Departments.Find(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public int Update(Department entity)
        {
            _context.Departments.Update(entity);
            return _context.SaveChanges();
        }
    }
}
