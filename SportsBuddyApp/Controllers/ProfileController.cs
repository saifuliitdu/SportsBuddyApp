using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SportsBuddy.Data;
using System.Threading.Tasks;
using SportsBuddy.Models.ViewModel;

namespace CutOutWizWebApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private string Root_Directory;
        public ProfileController(IConfiguration configuration,ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            Root_Directory = _configuration.GetSection("AppSettings").GetSection("Root_Directory").Value;
        }
        //public async Task<IActionResult> YourProfile()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    ProfileViewModel profile = new ProfileViewModel();
        //    if (user != null)
        //    {
        //        profile.Name = user.Name ?? "";
        //        profile.LastName = user.LastName ?? "";
        //        profile.FirstName = user.FirstName ?? "";
        //        profile.Email = user.Email ?? "";
        //        profile.Mobile = user.PhoneNumber ?? "";
        //        profile.ProfilePicture = user.ProfilePicture ?? "";
        //    }
        //    return View(profile);
        //}
        //public async Task<IActionResult> SaveProfile(ProfileViewModel profile)
        //{
        //    try
        //    {
        //        var user =await _userManager.GetUserAsync(User);
        //        if (user != null)
        //        {
        //            if (!_context.Users.Where(x=>x.PhoneNumber==profile.Mobile && x.Email!=user.Email).Any())
        //            {
        //                user.PhoneNumber = profile.Mobile ?? "";
        //                user.CompanyName = profile.CompanyName ?? "";
        //                user.OfficeAddress = profile.OfficeAddress ?? "";
        //                user.CompanyWebsite = profile.CompanyWebsite ?? "";
        //                user.OthersMemberEmail = profile.OthersMemberEmail ?? "";

        //                _context.SaveChanges();
        //                if (!string.IsNullOrEmpty(profile.Password) && !string.IsNullOrEmpty(profile.NewPassword) && !string.IsNullOrEmpty(profile.ConfirmPassword) && profile.NewPassword.Equals(profile.ConfirmPassword))
        //                {
        //                    await _userManager.ChangePasswordAsync(user, profile.Password, profile.NewPassword);
        //                    await _signInManager.SignOutAsync();
        //                    return Json("pass");
        //                }

        //                return Json("success");
        //            }
        //            else
        //            {
        //                return Json("Phone number already used.");
        //            }
        //        }
        //        else
        //        {
        //            Helper.Log("error");
        //            return Json("error");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Helper.Log(e.ToString());
        //        return Json("error");
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> updatePassword(ProfileViewModel model) {
        //    var user = _userManager.GetUserAsync(User).Result;
        //    if(!string.IsNullOrEmpty(model.Password) && (model.NewPassword == model.ConfirmPassword))
        //    {
        //        await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
        //        await _signInManager.SignOutAsync();
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json(false);
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> UploadProfilePicture() {           
        //    var user = _userManager.GetUserAsync(User).Result;
        //    var file = Request.Form.Files[0];
        //    try
        //    {
        //        var uploadFolderUrl = $"{Root_Directory}/{user.Email}/ProfilePictures";
        //        if (file != null)
        //        {                  
        //            if (!Directory.Exists(uploadFolderUrl))
        //                Directory.CreateDirectory(uploadFolderUrl);
        //            var path = Path.Combine(uploadFolderUrl);
        //            using (var fileStream = new FileStream(Path.Combine(path, $"{user.Id}.jpg"), FileMode.Create))
        //            {

        //                await file.CopyToAsync(fileStream);
        //            }

        //        }
        //        var imageByte = Helper.GetImage($"{uploadFolderUrl}/{user.Id}.jpg");
        //        user.ProfilePicture = $"{uploadFolderUrl}/{user.Id}.jpg";
        //           await _userManager.UpdateAsync(user);

        //        return Json(imageByte);
        //    }
        //    catch(Exception ex)
        //    {
        //        Helper.Log(ex.ToString());
        //        return Json(false);
        //    }

        //}
    }
}