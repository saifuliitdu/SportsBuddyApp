﻿using System.Linq;
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
        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public IActionResult Index()
        {
            var allActivities = _activityService.GetAllActivities();

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
                _activityService.AddAnActivity(recretionalActivity);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public IActionResult Edit(int activityId)
        {
            var activity = _activityService.GetActivityById(activityId);

            return View(activity);
        }
        [HttpPost]
        public IActionResult EditActivity(RecretionalActivity recretionalActivity)
        {
            try
            {
                bool result = _activityService.UpdateActivity(recretionalActivity);
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
                bool result = _activityService.DeleteActivity(activityId);
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