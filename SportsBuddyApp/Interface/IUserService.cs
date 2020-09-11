using SportsBuddy.Data;
using SportsBuddy.Models;
using SportsBuddyApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddyApp.Interface
{
    public interface IUserService
    {
        bool AddUser(ApplicationUser user);
        ApplicationUser GetUserById(string Id);
        IEnumerable<ApplicationUser> GetAllUsers();
        bool UpdateUser(ApplicationUser user);
        bool DeleteUser(string Id);
    }
}
