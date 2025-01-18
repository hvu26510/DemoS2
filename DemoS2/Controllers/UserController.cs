using DemoS2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DemoS2.Models.ViewModels;
using System.Reflection.Metadata;
using DemoS2.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DemoS2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id) 
        { 
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) { 
                return NotFound();
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FullName = model.FullName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) { 
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
             
            }
            return View(model);
        }


        [Authorize(Policy = "DeletePolicy")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
            { return NotFound(); }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ManageUserClaim(string userId)
        {
            //kiem tra user co trong db hay khong
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return NotFound(); };

            // lay ra cac claim user hien co
            var claimsUser = await _userManager.GetClaimsAsync(user);

            //tao viewModel theo thong tin vua lay ra
            var model = new UserClaimsViewModel
            {
                UserID = user.Id,
                Claims = ClaimsStore.GetAllClaims().Select(claim => new UserClaim
                {
                    ClaimType = claim.Type,
                    IsSelected = claimsUser.Any(c => c.Type == claim.Type)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaim(UserClaimsViewModel model)
        {
            //kiem tra user co trong db hay khong
            var user = await _userManager.FindByIdAsync(model.UserID);
            if (user == null) { return NotFound(); };

            var existClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in existClaims) {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            var selectedClaims = model.Claims.Where(c => c.IsSelected).Select(c=> new Claim(c.ClaimType,c.ClaimType));
            foreach (var claim in selectedClaims)
            {
                await _userManager.AddClaimAsync(user, claim);
            }

            return RedirectToAction("Index");

        }
    }
}
