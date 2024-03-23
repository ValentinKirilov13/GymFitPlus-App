using GymFitPlus.Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ApplicationUserConstants;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table with registered application users")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        [MaxLength(NameMaxLenght)]
        [Comment("User first name")]
        [PersonalData]
        public string? FirstName { get; set; }

        [MaxLength(NameMaxLenght)]
        [Comment("User last name")]
        [PersonalData]
        public string? LastName { get; set; }

        [Comment("User birth date")]
        [Column(TypeName = "date")]
        [PersonalData]
        public DateTime? BirthDate { get; set; }

        [Comment("User gender")]
        [PersonalData]
        public GenderType? Gender { get; set; }

        [Comment("User profile image")]
        [PersonalData]
        public byte[]? Image { get; set; }

        [MaxLength(UrlMaxLenght)]
        [Comment("Link to user Facebook account")]
        [PersonalData]
        public string? FacebookUrl { get; set; }

        [MaxLength(UrlMaxLenght)]
        [Comment("Link to user Instagram account")]
        [PersonalData]
        public string? InstagramUrl { get; set; }

        [MaxLength(UrlMaxLenght)]
        [Comment("Link to user YouTube account")]
        [PersonalData]
        public string? YouTubeUrl { get; set; }

        public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
        public ICollection<FitnessProgram> FitnessPrograms { get; set; } = new List<FitnessProgram>();
        public ICollection<UserSatistics> UserSatistics { get; set; } = new List<UserSatistics>();
    }
}
