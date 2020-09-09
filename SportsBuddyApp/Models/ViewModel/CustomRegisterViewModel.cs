using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddy.Models.ViewModel
{
    public class CustomRegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "The email is not valid")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Enter a valid email address")]
        public string RegEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[\w~@#$%^&*+=`|{}:;!.?\""()\[\]-]{6,25}$", ErrorMessage = "Password must contain at least one uppercase, one lowercase letter and one number")]
        [MinLength(6, ErrorMessage = "Password length must be at least 6")]
        [MaxLength(25, ErrorMessage = "Password can not be more than 25 characters long")]
        public string RegPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("RegPassword", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
