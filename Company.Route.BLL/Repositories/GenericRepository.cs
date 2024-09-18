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
    public class GenericRepository<T> : IGenreicRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(T entity)
        {
            _context.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _context.Remove(entity);
            return _context.SaveChanges();
        }

        public T Get(int? id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public int Update(T entity)
        {
            _context.Update(entity);
            return _context.SaveChanges();
        }
    }
}
