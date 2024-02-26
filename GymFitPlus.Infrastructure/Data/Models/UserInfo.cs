using GymFitPlus.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GymFitPlus.Infrastructure.Constants.DataConstants.UserInfoConstants;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table of users personal information")]
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Comment("User identifier")]
        public string Id { get; set; } = string.Empty;

        [Required]
        [MaxLength(NameMaxLenght)]
        [Comment("User first name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(NameMaxLenght)]
        [Comment("User last name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Comment("User birth date")]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Comment("User gender")]
        public GenderType Gender { get; set; }

        [MaxLength(UrlMaxLenght)]
        [Comment("Link to user profile image")]
        public string? ImgUrl { get; set; }

        [MaxLength(UrlMaxLenght)]
        [Comment("Link to user Facebook account")]
        public string? FacebookUrl { get; set; }

        [MaxLength(UrlMaxLenght)]
        [Comment("Link to user Instagram account")]
        public string? InstagramUrl { get; set; }

        [MaxLength(UrlMaxLenght)]
        [Comment("Link to user YouTube account")]
        public string? YouTubeUrl { get; set; }
    }
}
