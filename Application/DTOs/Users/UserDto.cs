namespace Application.DTOs.Users
{
    using Domain.Enums;
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;

        public string? Gander { get; set; }

        public DateTime? DateOfBitrh { get; set; }

        public string? RegionOfResidence { get; set; }

        public string? Bio { get; set; }
    }
}
