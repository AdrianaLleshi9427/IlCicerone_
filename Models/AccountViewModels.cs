using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IlCicerone.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IlCicerone.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required]

        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required]

        [Display(Name = "Surname")]
        public string Surname { get; set; }


        [Required]

        [Display(Name = "UserName")]
        public string UserName { get; set; }






        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }



        public string Gender { get; set; }

        //[DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
        //    ApplyFormatInEditMode = true)]
        //public DateTime Birthday { get; set; }

        public int YearlyTours { get; set; }

        public int DistancePreferences { get; set; }

        public int GroupSize { get; set; }

        public bool CityOrNature { get; set; }

        public bool SeaOrMountain { get; set; }

        public bool UseOwnVehicle { get; set; }

        public string PreferedTrasport { get; set; }

        public float? BudgetLimit { get; set; }

        public int TourDuration { get; set; }



        //Mapping country
        public int CountryId { get; set; }
        public virtual List<Country> Countrys { get; set; }

        //Mapping City
        public int CityId { get; set; }
        public virtual List<City> Cities { get; set; }



        //Mapping language
        public int LanguageId { get; set; }
        public virtual List<Language> Languages { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class CreateRoleViewModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Name { get; set; }

        public IEnumerable<ControllerDescription> SelectedControllers { get; set; }

        public IEnumerable<ControllerDescription> Controllers { get; set; }
    }

    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Name { get; set; }

        public IEnumerable<RoleAccess> RoleAccesses { get; set; }

        public IEnumerable<ControllerDescription> SelectedControllers { get; set; }

        public IEnumerable<ControllerDescription> Controllers { get; set; }
    }

    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }

    public class EditUserRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        public string UserName { get; set; }

        [Required]
        public IEnumerable<string> SelectedRoles { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
