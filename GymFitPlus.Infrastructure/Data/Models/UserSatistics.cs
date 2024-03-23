using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table with statistics of users")]
    public class UserSatistics
    {
        [Key]
        [Comment("Statistics identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Statistics owner")]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [Comment("Date Of Мeasurements")]
        [Column(TypeName = "date")]
        [PersonalData]
        public DateTime DateOfМeasurements { get; set; }

        [Required]
        [Comment("Weight of user in kilograms")]
        public double Weight { get; set; }

        [Required]
        [Comment("Height of user in meters")]
        public double Height { get; set; }

        [Required]
        [Comment("Chest circumference of user in centimeters")]
        public double ChestCircumference { get; set; }

        [Required]
        [Comment("Back circumference of user in centimeters")]
        public double BackCircumference { get; set; }

        [Required]
        [Comment("Right arm circumference of user in centimeters")]
        public double RightArmCircumference { get; set; }

        [Required]
        [Comment("Left arm circumference of user in centimeters")]
        public double LeftArmCircumference { get; set; }

        [Required]
        [Comment("Waist circumference of user in centimeters")]
        public double WaistCircumference { get; set; }

        [Required]
        [Comment("Gluteus circumference of user in centimeters")]
        public double GluteusCircumference { get; set; }

        [Required]
        [Comment("Right leg circumference of user in centimeters")]
        public double RightLegCircumference { get; set; }

        [Required]
        [Comment("Left leg circumference of user in centimeters")]
        public double LeftLegCircumference { get; set; }

        [Required]
        [Comment("Right calf circumference of user in centimeters")]
        public double RightCalfCircumference { get; set; }

        [Required]
        [Comment("Left calf circumference of user in centimeters")]
        public double LeftCalfCircumference { get; set; }
    }
}
