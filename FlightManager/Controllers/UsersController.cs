using FlightManager.Areas.Identity.Pages.Account;
using FlightManager.Data.Models;
using FlightManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<User> userManager;
        public UsersController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(userManager.Users);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel.InputModel input)
        {
            if(ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = input.Email,
                    Email = input.Email,
                    Address = input.Address,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    PersonalNo = input.PersonalNo
                };

                var result = await userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Errors(result);
                }
            }
            return View(input);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var model = new UserEditViewModel()
                {
                    Id = user.Id,
                    Address = user.Address,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PersonalNo = user.PersonalNo
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditViewModel model)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "User Not Found");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                user.Address = model.Address;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PersonalNo = model.PersonalNo;
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
            }
            
            return View(user);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("Index", userManager.Users);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
