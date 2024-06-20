using System.Threading.Tasks;
using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email
                };

                var result = await _userService.RegisterUserAsync(user, model.Password, model.Role);
                if (result)
                {
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, "User already exists");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.AuthenticateUserAsync(model.Username, model.Password);
                if (user != null)
                {
                    // W tym miejscu można ustawić autoryzację
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid credentials");
            }
            return View(model);
        }
    }
}
