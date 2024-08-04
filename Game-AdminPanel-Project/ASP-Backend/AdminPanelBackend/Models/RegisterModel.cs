using System.ComponentModel.DataAnnotations;

namespace AdminPanelBackend.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
