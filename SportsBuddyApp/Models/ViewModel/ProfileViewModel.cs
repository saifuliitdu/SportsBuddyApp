using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddy.Models.ViewModel
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string CompanyName { get; set; }        public string OfficeAddress { get; set; }        public string CompanyWebsite { get; set; }        public string OthersMemberEmail { get; set; }
        public string ProfilePicture { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
