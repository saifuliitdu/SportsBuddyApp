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
    public class UserService : IUserService
    {
        ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public bool AddUser(ApplicationUser user)
        {
            _context.Users.Add(user);
            return SaveChanges();
        }

        public bool DeleteUser(string Id)
        {
            var user = _context.Users.FirstOrDefault(f=>f.Id == Id);
            _context.Users.Remove(user);
            return SaveChanges();
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _context.Users.Include(x=>x.Region).ToList();
        }

        public ApplicationUser GetUserById(string Id)
        {
            var user = _context.Users.Include(x=>x.Region).FirstOrDefault(f=>f.Id == Id);
            return user;
        }

        public bool UpdateUser(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return SaveChanges();
        }

        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
