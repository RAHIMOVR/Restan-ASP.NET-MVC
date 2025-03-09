using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using restan.Models; // Ensure this is the correct path to your AppUser model
using restan.Areas.Admin.ViewModels; // Ensure this is the correct path to your ViewModels
using restan.Enums; // Ensure this is the correct path to your Enums (UserRole enum)
using System.Threading.Tasks;

namespace restan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: /Admin/Account/Register
        [HttpGet]
        public IActionResult Register() => View();

        // POST: /Admin/Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NormalizedUserName = model.UserName,
                    Name = model.Name,
                    Surname = model.Surname,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Add the user to the default role
                    await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());

                    // Sign in the user after successful registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                // If registration failed, add errors to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, redisplay the form with validation errors
            return RedirectToAction("Index", "Home", new { area = "default" });

        }

        // POST: /Admin/Account/CreateRoles
        [HttpPost]
        public async Task<IActionResult> CreateRoles()
        {
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString()
                    });
                }
            }

            return Content("Roles created successfully.");
        }

        // GET: /Admin/Account/Login
        [HttpGet]
        public IActionResult Login() => View();

        // POST: /Admin/Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                // If login failed, show error
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we got this far, redisplay the form with validation errors
            return RedirectToAction("Index", "Home", new { area = "default" });

        }

        // POST: /Admin/Account/Logout
        [HttpPost]
        [Authorize] // Only logged-in users can log out
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "default" });


        }

        // GET: /Admin/Account/AccessDenied
        [HttpGet]
        [Authorize] // Optional, depending on your app's structure
        public IActionResult AccessDenied()
        {
            return View(); // Create a view for access denied scenarios
        }
    }
}
