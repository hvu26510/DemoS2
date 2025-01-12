using System.ComponentModel.DataAnnotations;

namespace DemoS2.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email {  get; set; }

        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName {  get; set; }
    }
}
