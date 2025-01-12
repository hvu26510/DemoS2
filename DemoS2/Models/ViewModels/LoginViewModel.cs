using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace DemoS2.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Điền mail đê")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Điền cái password vào")]
        public string Password { get; set; }

        public bool RememberMe {  get; set; }


    }
}
