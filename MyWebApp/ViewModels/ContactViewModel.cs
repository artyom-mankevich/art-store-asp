using System.ComponentModel.DataAnnotations;

namespace MyWebApp.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Too short")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [MaxLength(250)]
        public string Message { get; set; }
    }
}