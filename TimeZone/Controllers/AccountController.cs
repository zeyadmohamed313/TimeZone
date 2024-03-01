using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager 
            , SignInManager<ApplicationUser>signInManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TryLogin(LoginViewModel user)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Name, user.Password, user.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View("Index",user);
            
        }
    }
}
