using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsBuddy.Data;
using SportsBuddyApp.Models.ViewModel;
using SportsBuddyApp.Interface;
using SportsBuddy.Models;
using System;

namespace CutOutWizWebApp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ActivityController : Controller
    {
        private readonly IUserActivityService _userActivityService;
        public ActivityController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }

        public IActionResult Index()
        {
            var allActivities = _userActivityService.GetActivities();

            return View(allActivities);
        }

        public IActionResult Create()
        {
            RecretionalActivity recretionalActivity = new RecretionalActivity();

            return View(recretionalActivity);
        }

        [HttpPost]
        public IActionResult CreateActivity(RecretionalActivity recretionalActivity)
        {
            try
            {
                _userActivityService.AddAnActivity(recretionalActivity);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public IActionResult Edit(int activityId)
        {
            var activity = _userActivityService.GetActivities().Where(x => x.ActivityId == activityId).FirstOrDefault();

            return View(activity);
        }
        [HttpPost]
        public IActionResult EditActivity(RecretionalActivity recretionalActivity)
        {
            try
            {
                bool result = _userActivityService.UpdateActivity(recretionalActivity);
                if (result)
                    return RedirectToAction("Index");
                else
                    return View(recretionalActivity);
            }
            catch (Exception e)
            {
                return View(recretionalActivity);
            }
        }
        public IActionResult Delete(int activityId)
        {
            try
            {
                bool result = _userActivityService.DeleteActivity(activityId);
                if (result)
                    return RedirectToAction("Index");
                else
                    return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}