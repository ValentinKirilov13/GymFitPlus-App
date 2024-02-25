using System.ComponentModel.DataAnnotations;

namespace GymFitPlus.Core.ViewModels.UserInfoViewModels
{
    public class UserInfoViewModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string BirthDate { get; set; } = string.Empty;

        [Required]
        public int Gender { get; set; }

        [MaxLength(200)]
        public string? ImgUrl { get; set; }

        [MaxLength(200)]
        public string? FacebookUrl { get; set; }

        [MaxLength(200)]
        public string? InstagramUrl { get; set; }

        [MaxLength(200)]
        public string? YouTubeUrl { get; set; }
    }
}
