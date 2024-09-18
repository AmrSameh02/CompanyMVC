using Company.Route.BLL.Interfaces;
using Company.Route.BLL.Repositories;
using Company.Route.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department model)
        {
            if(ModelState.IsValid)
            {
                var count = _departmentRepository.Add(model);

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);

        }

        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentRepository.Get(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();
            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var count = _departmentRepository.Update(model);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
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
            if (id is null) return BadRequest();
            var department = _departmentRepository.Get(id.Value);

            if (department is null)return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (!ModelState.IsValid)
                {
                    var count = _departmentRepository.Delete(model);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }

    }
}

