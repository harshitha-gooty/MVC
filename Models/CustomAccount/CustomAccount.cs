using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomAccount
{
    public class CustomAccount
    {
    }
    public class Register
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be of minimum length of {2}", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "The password and confirmPassword doesnt match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not valid email address")]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; }
        public Boolean IsActive { get; set; }

    }

    public class Login
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Boolean RememberMe { get; set; }
    }

    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserRoleMapping
    {
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
