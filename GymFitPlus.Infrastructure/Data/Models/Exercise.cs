using GymFitPlus.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Infrastructure.Constants.DataConstants.ExerciseConstants;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table of Exercise")]
    public class Exercise
    {
        [Key]
        [Comment("Exercise identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        [Comment("Exercise name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(DescriptionMaxLenght)]
        [Comment("Exercise description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(VideoUrlMaxLenght)]
        [Comment("Exercise video link")]
        public string VideoUrl { get; set; } = string.Empty;

        [Required]
        [Comment("Exercise muscle group target")]
        public MuscleGroup MuscleGroup { get; set; }

        [Comment("Exercise status")]
        public bool IsDelete { get; set; } = false;

        public ICollection<FitnessProgramExercise> FitnessProgramsExercises { get; set; } = new List<FitnessProgramExercise>();
    }
}
