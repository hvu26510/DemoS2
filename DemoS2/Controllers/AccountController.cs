using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DemoS2.Models;
using DemoS2.Models.ViewModels;
using System.Diagnostics.Contracts;

namespace DemoS2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signinManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) {
                //tạo đối tượng Application user
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) 
                { 
                    await _signinManager.SignInAsync(user, isPersistent: false);
                    RedirectToAction("Index", "Home");
                }
                foreach (var e in result.Errors) {
                    ModelState.AddModelError(string.Empty, e.Description);
                }
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
                var result = await _signinManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded) 
                { 
                   RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty,"Sai thong tin roi");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
