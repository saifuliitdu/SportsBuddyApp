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
    public class RegionController : Controller
    {
        private readonly IRegionService _regionService;
        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        public IActionResult Index()
        {
            var allRegions = _regionService.GetAllRegions();

            return View(allRegions);
        }

        public IActionResult Create()
        {
            Region region = new Region();

            return View(region);
        }

        [HttpPost]
        public IActionResult CreateRegion(Region region)
        {
            try
            {
                _regionService.AddRegion(region);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public IActionResult Edit(int regionId)
        {
            var region = _regionService.GetRegionById(regionId);

            return View(region);
        }
        [HttpPost]
        public IActionResult EditRegion(Region region)
        {
            try
            {
                bool result = _regionService.UpdateRegion(region);
                if (result)
                    return RedirectToAction("Index");
                else
                    return View(region);
            }
            catch (Exception e)
            {
                return View(region);
            }
        }
        public IActionResult Delete(int regionId)
        {
            try
            {
                bool result = _regionService.DeleteRegion(regionId);
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