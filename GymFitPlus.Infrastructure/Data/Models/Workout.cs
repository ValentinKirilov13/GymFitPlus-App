using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GymFitPlus.Infrastructure.Constants.DataConstants.WorkoutConstants;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table with users workouts")]
    public class Workout
    {
        [Key]
        [Comment("Workout identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Workout owner")]
        public Guid UserId { get; set; }

        [Required]
        [Comment("Fitness program that is used in workout")]
        public int FitnessProgramId { get; set; }

        [Required]
        [Comment("Date of workout")]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [Comment("Duration of workout in minutes")]
        public int Duration { get; set; }

        [MaxLength(NoteMaxLenght)]
        [Comment("User note on workout")]
        public string? Note { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(FitnessProgramId))]
        public FitnessProgram FitnessProgram { get; set; } = null!;
    }
}
