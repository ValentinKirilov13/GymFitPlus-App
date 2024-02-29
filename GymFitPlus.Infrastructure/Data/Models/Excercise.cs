using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ExcerciseConstants;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table of excercise")]
    public class Excercise
    {
        [Key]
        [Comment("Excercise identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        [Comment("Excercise name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(DescriptionMaxLenght)]
        [Comment("Excercise description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Excercise image")]
        public byte[] Image { get; set; } = null!;

        [Comment("Excercise status")]
        public bool IsDelete { get; set; } = false;

        public ICollection<FitnessProgramExcercise> FitnessProgramsExcercises { get; set; } = new List<FitnessProgramExcercise>();
    }
}
