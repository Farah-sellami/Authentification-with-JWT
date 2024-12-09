using System.ComponentModel.DataAnnotations;

namespace JobExpressBack.Models.DTOs
{
    public class RegisterModel
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Le mot de passe et sa confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Rôle")]
        public string Role { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public string? PhotoProfile { get; set; }
    }
}
