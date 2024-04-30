namespace Application.DTOs.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;

        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? RegionOfResidence { get; set; }

        public string? Bio { get; set; }
    }
}
