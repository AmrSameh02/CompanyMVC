using Company.Route.BLL;
using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(//IEmployeeRepository employeeRepository,
        //    IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;

            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;

        }
        public IActionResult Index(string InputSearch)
        {
            var employee = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(InputSearch))
            {
                employee = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employee = _unitOfWork.EmployeeRepository.GetByName(InputSearch);
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Add(model);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public IActionResult Details(int? id,string viewName= "Details")
        {
            if(id is null)
                return BadRequest();
            var employeee = _unitOfWork.EmployeeRepository.Get(id.Value);

            if(employeee is null)
                return NotFound();
            return View(viewName, employeee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Employee model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    _unitOfWork.EmployeeRepository.Update(model);
                    var count = _unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int? id,Employee model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (!ModelState.IsValid)
                {
                    _unitOfWork.EmployeeRepository.Delete(model);
                    var count = _unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return View(model);
        }
    }
}
