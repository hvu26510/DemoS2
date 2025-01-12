using System.ComponentModel.DataAnnotations;

namespace DemoS2.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string FullName {  get; set; }

        [Required]
        public string Email { get; set; }

    }
}
