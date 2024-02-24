using GymFitPlus.Infrastructure.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table of users personal information")]
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Comment("User identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Comment("User first name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Comment("User last name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Comment("User birth date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Comment("User gender")]
        public GenderType Gender { get; set; }

        [MaxLength(20)]
        [Comment("User phone number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        [Comment("User email adress")]
        public string Email { get; set; } = string.Empty;

        [Comment("User email to be public or not")]
        public bool ShowEmail { get; set; }

        [MaxLength(200)]
        [Comment("Link to user Facebook account")]
        public string? FacebookUrl { get; set; }

        [MaxLength(200)]
        [Comment("Link to user Instagram account")]
        public string? InstagramUrl { get; set; }

        [MaxLength(200)]
        [Comment("Link to user YouTube account")]
        public string? YouTubeUrl { get; set; }
    }
}
