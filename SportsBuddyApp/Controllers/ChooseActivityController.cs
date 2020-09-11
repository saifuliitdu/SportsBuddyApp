using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsBuddy.Data;
using SportsBuddyApp.Models.ViewModel;
using SportsBuddyApp.Interface;
using SportsBuddy.Models;
using System;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.Generic;

namespace CutOutWizWebApp.Controllers
{
    [Authorize]
    public class ChooseActivityController : Controller
    {
        private readonly IUserActivityService _userActivityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IActivityService _activityService;
        public ChooseActivityController(IUserActivityService userActivityService, UserManager<ApplicationUser> userManager, IActivityService activityService)
        {
            _userActivityService = userActivityService;
            _userManager = userManager;
            _activityService = activityService;
        }

        public IActionResult Index()
        {
            List<ChooseActivityViewModel> chooseActivites = new List<ChooseActivityViewModel>();
            List<RecretionalActivity> allActivities = new List<RecretionalActivity>();
            var allUserActivities = _userActivityService.GetAllUserActivities();
            var user = _userManager.GetUserAsync(User).Result;
            allActivities = _activityService.GetAllActivities().ToList();

            allActivities.ForEach(x => {
                var userActivity = _userActivityService.GetUserActivityById(user.Id, x.ActivityId);
                ChooseActivityViewModel chooseActivity = null;
                if (userActivity != null)
                    chooseActivity = new ChooseActivityViewModel { Activity = x, IsChoosen = x.ActivityId == userActivity.ActivityId };
                else
                    chooseActivity = new ChooseActivityViewModel { Activity = x };

                chooseActivites.Add(chooseActivity);
            });

            return View(chooseActivites);
        }
        public IActionResult Choose(int activityId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var activity = _activityService.GetActivityById(activityId);

            bool result = _userActivityService.ChooseAnActivity(user, activity);

            return RedirectToAction("Index");
        }
        public IActionResult Rating(int activityId)
        {
            if (activityId <= 0) return RedirectToAction("Index");
            var user = _userManager.GetUserAsync(User).Result;
            var userActivity = _userActivityService.GetUserActivityById(user.Id, activityId);
            RankActivityViewModel rankActivityViewModel = new RankActivityViewModel {ActivityId = activityId, Activity = userActivity.Activity, Rating = (int)userActivity.Rating};

            return View(rankActivityViewModel);
        }

        [HttpPost]
        public IActionResult RatingActivity(RankActivityViewModel model)
        {
            if (model.Rating > 5) { @ViewData["error"] = "Please input 1 to 5 rating."; return RedirectToAction("Rank", new { activityId = model.ActivityId }); }
            var user = _userManager.GetUserAsync(User).Result;
            var activity = _activityService.GetActivityById(model.ActivityId);
            _userActivityService.SetRankAnActivity(user, activity, (Rating)model.Rating);
            return RedirectToAction("Index");
        }
    }
}