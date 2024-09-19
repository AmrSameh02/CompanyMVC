using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route.BLL.Interfaces
{
    public interface IGenreicRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int? id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
