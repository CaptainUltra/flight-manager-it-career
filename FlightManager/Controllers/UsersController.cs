using FlightManager.Areas.Identity.Pages.Account;
using FlightManager.Data.Models;
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
        public UsersController(UserManager<User> usrMgr)
        {
            this.userManager = usrMgr;
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
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(input);
        }
    }
}
