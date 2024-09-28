using Company.Route.DAL.Models;
using Company.Route.PL.Helper;
using Company.Route.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Route.PL.Controllers
{
    [Authorize(Roles = "Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(string InputSearch)
		{
            var userList = new List<UserViewModel>();

            if (string.IsNullOrEmpty(InputSearch))
            {
                var users = await _userManager.Users.ToListAsync();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user); // Fetch roles asynchronously
                    userList.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Roles = roles // Assign fetched roles
                    });
                }
            }
            else
            {
                var users = await _userManager.Users
                    .Where(U => U.Email.ToLower().Contains(InputSearch.ToLower()))
                    .ToListAsync();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user); // Fetch roles asynchronously
                    userList.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Roles = roles // Assign fetched roles
                    });
                }
            }

            return View(userList);
        }


        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

			var userFromDb = await _userManager.FindByIdAsync(id);
            if (userFromDb is null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(userFromDb);
            var user =  new UserViewModel()
			{
				Id = userFromDb.Id,
				FirstName = userFromDb.FirstName,
				LastName = userFromDb.LastName,
				Email = userFromDb.Email,
				Roles = roles
			};

            return View(viewName,user);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {

            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(id);
                    if(userFromDb is null)
                        return NotFound();         
                    
                    userFromDb.FirstName = model.FirstName;
                    userFromDb.LastName = model.LastName;
                    userFromDb.Email = model.Email;
                    var result = await _userManager.UpdateAsync(userFromDb);
                    if (result.Succeeded)
                        {
                           return RedirectToAction(nameof(Index));
                        }
                }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb is null)
                    return NotFound();

                var result = await _userManager.DeleteAsync(userFromDb);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(model);
        }

    }
}
