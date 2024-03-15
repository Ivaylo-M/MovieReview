namespace Application.DTOs.Users
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.User;
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(PasswordMinLength)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
