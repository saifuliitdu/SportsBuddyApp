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
                    user.DateOfBirth = GetDateTimeFromDateString(profile.DateOfBirth);
                    user.Region = region;

                    _context.SaveChanges();
                    
                    return RedirectToAction("YourProfile");
                 
                }
                else
                {
                    return Json("error");
                }
            }
            catch (Exception e)
            {
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
    }
}