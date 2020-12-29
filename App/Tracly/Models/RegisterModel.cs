using System.ComponentModel.DataAnnotations;

namespace App.Tracly.Models
{
    public class RegisterModel
    {
            [Required]  
            [EmailAddress]  
            [Display(Name = "Email")]  
            public string Email { get; set; }  
  
            [Required]  
            [StringLength(30, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 6)]
            //[RegularExpression(@"/(?=.*[a-z])/", ErrorMessage = "Password must contain at least one lower case letter.")]
            [DataType(DataType.Password)]  
            [Display(Name = "Password")]  
            public string Password { get; set; }  
  
            [DataType(DataType.Password)]  
            [Display(Name = "Confirm password")]  
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]  
            public string ConfirmPassword { get; set; }
    }
}