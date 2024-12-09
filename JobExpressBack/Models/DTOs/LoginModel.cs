using System.ComponentModel.DataAnnotations;

namespace JobExpressBack.Models.DTOs
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
