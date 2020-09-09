using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsBuddy.Data;
using SportsBuddyApp.Models.ViewModel;
using SportsBuddyApp.Interface;
using SportsBuddy.Models;
using System;
using Microsoft.AspNetCore.Identity;

namespace CutOutWizWebApp.Controllers
{
    [Authorize]
    public class ChooseActivityController : Controller
    {
        private readonly IUserActivityService _userActivityService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChooseActivityController(IUserActivityService userActivityService, UserManager<ApplicationUser> userManager)
        {
            _userActivityService = userActivityService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var allActivities = _userActivityService.GetActivities();

            //var user = _userManager.GetUserAsync(User).Result;
            //allActivities.Where(x => x.UserActivityRankings.Select(y => y.User).Where(p => p.Id == user.Id)).ToList();

            return View(allActivities);
        }

        public IActionResult Choose(int activityId)
        {
            var user =  _userManager.GetUserAsync(User).Result;
            var activity = _userActivityService.GetActivityById(activityId);

            bool result = _userActivityService.ChooseAnActivity(user, activity);

            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult CreateActivity(RecretionalActivity recretionalActivity)
        //{
        //    try
        //    {
        //        _userActivityService.AddAnActivity(recretionalActivity);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        return View();
        //    }
        //}

    }
}