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
    public class ActivityService : IActivityService
    {
        ApplicationDbContext _context;
        public ActivityService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public bool AddAnActivity(RecretionalActivity activity)
        {
            _context.Activities.Add(activity);
            return SaveChanges();
        }

        public bool DeleteActivity(int Id)
        {
            var activity = _context.Activities.FirstOrDefault(f=>f.ActivityId == Id);
            _context.Activities.Remove(activity);
            return SaveChanges();
        }

        public IEnumerable<RecretionalActivity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        public RecretionalActivity GetActivityById(int Id)
        {
            var activity = _context.Activities.FirstOrDefault(f=>f.ActivityId == Id);
            return activity;
        }


        public bool UpdateActivity(RecretionalActivity activity)
        {
            var existingActivity = GetActivityById(activity.ActivityId);
            if (existingActivity != null)
                existingActivity.ActivityName = activity.ActivityName;
            return SaveChanges();
            //_context.Entry(activity).State = EntityState.Modified;
            //return SaveChanges();
        }

        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
