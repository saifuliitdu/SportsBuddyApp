using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SportsBuddy.Data;
using System.Threading.Tasks;
using SportsBuddy.Models.ViewModel;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using SportsBuddy.Models;
using SportsBuddyApp.Interface;

namespace CutOutWizWebApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        public async Task<IActionResult> YourProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            user = _userService.GetUserById(user.Id);
            var regions = _context.Regions.ToList();
            ProfileViewModel profile = new ProfileViewModel();
            if (user != null)
            {
                profile.Name = user.Name ?? "";
                profile.LastName = user.LastName ?? "";
                profile.FirstName = user.FirstName ?? "";
                profile.Email = user.Email ?? "";
                profile.DateOfBirth = user.DateOfBirth.ToString("yyyy-MM-dd");
                profile.ProfilePicture = user.ProfilePicture ?? "";
                profile.RegionId = user.Region == null ? 0 : user.Region.RegionId;
                profile.Regions = GetRegionItemList(regions);
            }
            return View(profile);
        }
        private List<SelectListItem> GetRegionItemList(IEnumerable<Region> allReions)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            allReions.ToList().ForEach(x =>
            {
                var regionItem = new SelectListItem { Text = x.RegionName, Value = x.RegionId.ToString() };
                selectListItems.Add(regionItem);
            });

            return selectListItems;
        }
        public async Task<IActionResult> SaveProfile(ProfileViewModel profile)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var region = _context.Regions.FirstOrDefault(f => f.RegionId == profile.RegionId);
                    //if (!_context.Users.Where(x => x.Email != user.Email).Any())
                    //{
                    //user.PhoneNumber = profile.Mobile ?? "";
                    user.DateOfBirth = GetDateTimeFromDateString(profile.DateOfBirth);
                    user.Region = region;

                    _context.SaveChanges();
                    //if (!string.IsNullOrEmpty(profile.Password) && !string.IsNullOrEmpty(profile.NewPassword) && !string.IsNullOrEmpty(profile.ConfirmPassword) && profile.NewPassword.Equals(profile.ConfirmPassword))
                    //{
                    //    await _userManager.ChangePasswordAsync(user, profile.Password, profile.NewPassword);
                    //    await _signInManager.SignOutAsync();
                    //    return Json("pass");
                    //}

                    return RedirectToAction("YourProfile");
                    //}
                    //else
                    //{
                    //    return Json("Email already used.");
                    //}
                }
                else
                {
                    //Helper.Log("error");
                    return Json("error");
                }
            }
            catch (Exception e)
            {
                //Helper.Log(e.ToString());
                return Json("error");
            }
        }
        private DateTime GetDateTimeFromDateString(string date)
        {
            var arr = date.Split("-").ToList();
            var year = Convert.ToInt32(arr[0] ?? "");
            var month = Convert.ToInt32(arr[1] ?? "");
            var day = Convert.ToInt32(arr[2] ?? "");


            var dateTime = new DateTime(year, month, day);
            return dateTime;
        }
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