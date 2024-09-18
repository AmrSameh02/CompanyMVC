using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employee = _employeeRepository.GetAll();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                var count = _employeeRepository.Add(model);
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
            var employeee = _employeeRepository.Get(id.Value);

            if(employeee is null)
                return NotFound();
            return View(viewName, employeee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
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
                    var count = _employeeRepository.Update(model);
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
                    var count = _employeeRepository.Delete(model);
                    if(count > 0)
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
