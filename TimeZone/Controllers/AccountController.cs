using AutoMapper;
using BusinessLogicLayer.UnitOfWork;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager 
            , SignInManager<ApplicationUser> signInManager, IMapper mapper,
            IUnitOfWork unitOfWork, IConfiguration configuration
, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> TryLogin(LoginViewModel user)
        {
            if(ModelState.IsValid)
            {
                var UserFound = await _userManager.FindByNameAsync(user.UserName);
                if(UserFound == null)
                {
                    ModelState.AddModelError(string.Empty, "This Username is Not Exsist");
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var RolesForUser = await _userManager.GetRolesAsync(UserFound);                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if(UserFound!= null) 
                    ModelState.AddModelError(string.Empty, "The Password Is Invalid");
                }
            }
            return View("Login",user);
        }

       


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TryRegister(RegisterViewModel user)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser = _mapper.Map<ApplicationUser>(user);
                var result = await _userManager.CreateAsync(applicationUser, user.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(applicationUser, isPersistent: false);
                    AssignShoppingCartToUser(applicationUser.Id);
                    await _userManager.AddToRoleAsync(applicationUser, "User");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View("Register",user);
        }
        private void AssignShoppingCartToUser(string userid)
        {
            //var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _unitOfWork.ShoppingCartRepository.Add(userid);
            _unitOfWork.Commit();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback()
        {
            var externalLoginResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (externalLoginResult?.Principal == null)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            var externalUser = externalLoginResult.Principal;
            var email = externalUser.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                var newUser = new ApplicationUser
                {
                    EmailAddress = email,
                    UserName = email.Split('@')[0],
                    FirstName = email.Split('@')[0],
                    LastName = email.Split('@')[0],
                    Email = email
                };

                var createResult = await _userManager.CreateAsync(newUser);

                if (createResult.Succeeded)
                {
                    var roleExists = await _roleManager.RoleExistsAsync("User");

                    if (!roleExists)
                    {
                        var createRoleResult = await _roleManager.CreateAsync(new IdentityRole("User"));

                        if (!createRoleResult.Succeeded)
                        {
                            return RedirectToAction("ExternalLoginFailure");
                        }
                    }
                    var addToRoleResult = await _userManager.AddToRoleAsync(newUser, "User");

                    if (addToRoleResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, isPersistent: false);
                    }
                    else
                    {
                        return RedirectToAction("ExternalLoginFailure");
                    }
                }
                else
                {
                    return RedirectToAction("ExternalLoginFailure");
                }
            }
            else
            {
                await _signInManager.SignInAsync(existingUser, isPersistent: false);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
