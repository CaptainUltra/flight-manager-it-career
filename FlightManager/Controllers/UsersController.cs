using FlightManager.Areas.Identity.Pages.Account;
using FlightManager.Data.Models;
using FlightManager.HelperClasses;
using FlightManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string emailSearch, string firstNameSearch, string lastNameSearch, int? pageNumber)
        {
            if (emailSearch != null)
            {
                pageNumber = 1;
            }

            ViewData["EmailSearch"] = emailSearch;
            ViewData["FirstNameSearch"] = firstNameSearch;
            ViewData["LastNameSearch"] = lastNameSearch;

            var users = from u in userManager.Users
                               select u;
            if (!String.IsNullOrEmpty(emailSearch))
            {
                users = users.Where(u => u.Email.Contains(emailSearch));
            }
            if (!String.IsNullOrEmpty(firstNameSearch))
            {
                users = users.Where(u => u.FirstName.Contains(firstNameSearch));
            }
            if (!String.IsNullOrEmpty(lastNameSearch))
            {
                users = users.Where(u => u.LastName.Contains(lastNameSearch));
            }

            int pageSize = 3;
            return View(await PaginatedList<User>.CreateAsync(users, pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(UserCreateViewModel input)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = input.Email,
                    Email = input.Email,
                    Address = input.Address,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    PersonalNo = input.PersonalNo,
                    PhoneNumber = input.Phone
                };

                string role = input.Role == 1 ? "Admin" : "Employee";

                var result = await userManager.CreateAsync(user, input.Password);
                var result1 = await userManager.AddToRoleAsync(user, role);
                if (result.Succeeded && result1.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Errors(result);
                }
            }
            return View(input);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var role = userManager.GetRolesAsync(user).Result.First();
                var model = new UserEditViewModel()
                {
                    Id = user.Id,
                    Address = user.Address,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PersonalNo = user.PersonalNo,
                    Phone = user.PhoneNumber,
                    Role = role == "Admin" ? 1 : 2
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
                user.PhoneNumber = model.Phone;
                string role = model.Role == 1 ? "Admin" : "Employee";
                await userManager.RemoveFromRoleAsync(user, "Admin");
                await userManager.RemoveFromRoleAsync(user, "Employee");
                var result1 = await userManager.AddToRoleAsync(user, role);
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
