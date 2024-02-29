using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table of excercise in one fitness program")]
    public class FitnessProgramExcercise
    {
        [Required]
        [Comment("Fitness program identifier")]
        public int FitnessProgramId { get; set; }

        [Required]
        [ForeignKey(nameof(FitnessProgramId))]
        public FitnessProgram FitnessProgram { get; set; } = null!;

        [Required]
        [Comment("Excercise identifier")]
        public int ExcerciseId { get; set; }

        [Required]
        [ForeignKey(nameof(ExcerciseId))]
        public Excercise Excercise { get; set; } = null!;

        [Required]
        [Comment("Sets for the excercise")]
        public int Sets { get; set; }

        [Required]
        [Comment("Reps for the excercise")]
        public int Reps { get; set; }

        [Required]
        [Comment("Weigth for the excercise")]
        public int Weigth { get; set; }
    }
}