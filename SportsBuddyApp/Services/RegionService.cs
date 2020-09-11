using SportsBuddy.Data;
using SportsBuddy.Models;
using SportsBuddyApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsBuddyApp.Models.ViewModel;

namespace SportsBuddyApp.Services
{
    public class RegionService : IRegionService
    {
        ApplicationDbContext _context;
        public RegionService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public bool AddRegion(Region region)
        {
            _context.Regions.Add(region);
            return SaveChanges();
        }

        public bool DeleteRegion(int Id)
        {
            var region = _context.Regions.FirstOrDefault(f=>f.RegionId == Id);
            _context.Regions.Remove(region);
            return SaveChanges();
        }

        public IEnumerable<Region> GetAllRegions()
        {
            return _context.Regions.ToList();
        }

        public Region GetRegionById(int Id)
        {
            var region = _context.Regions.FirstOrDefault(f=>f.RegionId == Id);
            return region;
        }


        public bool UpdateRegion(Region region)
        {
            _context.Entry(region).State = EntityState.Modified;
            return SaveChanges();
        }

        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
