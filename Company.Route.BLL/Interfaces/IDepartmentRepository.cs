using Company.Route.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();

        Department Get(int? id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);
    }
}
