﻿using Company.Route.DAL.Models;
using Company.Route.PL.Helper;
using Company.Route.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager
			)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        [HttpGet]
       public IActionResult SignUp()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
            if (ModelState.IsValid)
            {
                try
                {
					var user = await _userManager.FindByNameAsync(model.UserName);
					if (user is null)
					{
						user = await _userManager.FindByEmailAsync(model.Email);
						if (user is null)
						{
							user = new ApplicationUser()
							{
								UserName = model.UserName,
								Email = model.Email,
								FirstName = model.FirstName,
								LastName = model.LastName,
								IsAgree = model.IsAgree
							};
							var result = await _userManager.CreateAsync(user, model.Password);
							if (result.Succeeded)
							{
								return RedirectToAction("SignIn");
							}

							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}

						}
						ModelState.AddModelError(string.Empty, "Email Is Already Exist");
						return View(model);
					}

					ModelState.AddModelError(string.Empty, "UserName Is Already Exist");
 
				}
				catch (Exception ex)
                {
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						var flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (flag)
						{
							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
							if(result.Succeeded)
							{
								return RedirectToAction("Index", "Home");
							}
						}
					}
					ModelState.AddModelError(string.Empty, "Invalid Login");
				}
				catch(Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}


		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				
				if (user is not null)
				{

					var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					var url = Url.Action("ResetPassword", "Account",new {email = model.Email, token = token}, Request.Scheme);

					var email = new Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = url
					};

					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}

				ModelState.AddModelError(string.Empty, "Invalid Operation, Please Try Again");

			}
			return View(model);
		}

		[HttpGet]
		public IActionResult CheckYourInbox()
		{
			return View();
		}


		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}
				}			
			}
			ModelState.AddModelError(string.Empty, "Invalid Operation, Please try again");

			return View(model);
		}
	
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}