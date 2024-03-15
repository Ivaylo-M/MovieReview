namespace Application.DTOs.Users
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.User;

    public class RegisterUserDto
    {
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(PasswordMinLength)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
