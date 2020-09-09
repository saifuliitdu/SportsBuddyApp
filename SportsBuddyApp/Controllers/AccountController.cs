using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CutOutWizWebApp.Models.AccountViewModels;
using Microsoft.AspNetCore.Cors;
using CutOutWizWebApp.Services;
using Microsoft.Extensions.Configuration;
using SportsBuddy.Data;
using SportsBuddy.Models.AccountViewModels;

namespace CutOutWizWebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [EnableCors("CORS")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(
            IConfiguration configuration,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(x => x.UserName == model.Email).FirstOrDefault();
                
                if (user == null)
                {
                    return Json(new { status = false, message = "This email is not registered", type = "message" });
                }
                if (user != null && !user.EmailConfirmed)
                {
                    var userole = await _userManager.GetRolesAsync(user);
                    if (userole[0] == "Admin")
                    {
                        var res = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                        if (res.Succeeded)
                        {
                            return Json(new { status = true, message = "/AdminPanel/Index", type = "url" });
                        }
                    }
                    return Json(new { status = false, message = "Please verify your E-mail address.", type = "message" });
                }
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var userole = await _userManager.GetRolesAsync(user);
                    if (userole[0] == "Admin")
                    {
                        await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                        return Json(new { status = true, message = "/AdminPanel/Index", type = "url" });
                    }
                     return Json(new { status = true, message = "/AdminPanel/Index", type = "url" });
                    //return Json(new { status = true, message = "/Upload/UploadPhoto", type = "url" });
                }
                else
                {
                    return Json(new { status = false, message = "Invalid login attempt.", type = "message" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Please enter valid credential.", type = "message" });
            }
        }
        

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AdminRegister(RegisterViewModel model)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.RegEmail, UserName = model.RegEmail };
                var result = await _userManager.CreateAsync(user, model.RegPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return Json(true);
                }
                else
                {
                    return Json(true);
                }
            }
            else
            {
                return Json(true);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });

            //if (ModelState.IsValid)
            //{
                var user = new ApplicationUser { Email = model.RegEmail, UserName = model.RegEmail, FirstName = model.FirstName, LastName = model.LastName };
                var result = await _userManager.CreateAsync(user, model.RegPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            //}
            //else
            //{
            //    return Json(true);
            //}
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        #endregion
    }
}
