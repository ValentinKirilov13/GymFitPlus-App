using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GymFitPlus.Infrastructure.Constants.DataConstants.FitnessProgramConstants;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table with fitness programs")]
    public class FitnessProgram
    {
        [Key]
        [Comment("Fitness program identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        [Comment("Fitness program name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("Fitness program creator/owner")]
        public Guid UserId { get; set; }

        [Comment("Excercise status")]
        public bool IsDelete { get; set; } = false;

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public ICollection<FitnessProgramExercise> FitnessProgramsExcercises { get; set; } = new List<FitnessProgramExercise>();
    }
}
