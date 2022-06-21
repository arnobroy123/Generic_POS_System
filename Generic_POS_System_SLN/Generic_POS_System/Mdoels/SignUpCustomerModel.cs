using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Mdoels
{
    public class SignUpCustomerModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please enter your First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Please enter your Email")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
