using AutoMapper;
using Company.Route.DAL.Models;
using Company.Route.PL.ViewModels;

namespace Company.Route.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();

        }
    }
}
