using AutoMapper;
using Company.Route.BLL;
using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Models;
using Company.Route.PL.Helper;
using Company.Route.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Company.Route.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork,
            IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string InputSearch)
        {


            var employee = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(InputSearch))
            {
                employee = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employee = await _unitOfWork.EmployeeRepository.GetByNameAsync(InputSearch);
            }

           var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employee);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                    
                if(model.Image is not null)
                {
                    model.ImageName = DocumentSettings.Upload(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id,string viewName= "Details")
        {
            if(id is null)
                return BadRequest();
            var employeee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if(employeee is null)
                return NotFound();

            var result = _mapper.Map<EmployeeViewModel>(employeee);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        { 
            var departments =await  _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            if(id is null) return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if(employee is null) return NotFound();
            var result = _mapper.Map<EmployeeViewModel>(employee);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    if(model.ImageName is not null)
                    {
                        DocumentSettings.Delete(model.ImageName, "images");
                    }
                    if(model.Image is not null)
                    {
                        model.ImageName = DocumentSettings.Upload(model.Image, "images");
                    }
                    var employee = _mapper.Map<Employee>(model);

                    _unitOfWork.EmployeeRepository.Update(employee);
                    var count = await _unitOfWork.CompleteAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null) return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound();
            var employeeView = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int? id,EmployeeViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (!ModelState.IsValid)
                {


                    var employee = _mapper.Map<Employee>(model);
                    _unitOfWork.EmployeeRepository.Delete(employee);
                    var count = await _unitOfWork.CompleteAsync();
                    if (count > 0)
                    {
                        if (model.ImageName is not null)
                        {
                            DocumentSettings.Delete(model.ImageName, "images");
                        }
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
