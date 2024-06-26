﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table of Exercise in one fitness program")]
    public class FitnessProgramExercise
    {
        [Required]
        [Comment("Fitness program order")]
        public int Order { get; set; }

        [Required]
        [Comment("Fitness program identifier")]
        public int FitnessProgramId { get; set; }

        [Required]
        [ForeignKey(nameof(FitnessProgramId))]
        public FitnessProgram FitnessProgram { get; set; } = null!;

        [Required]
        [Comment("Exercise identifier")]
        public int ExerciseId { get; set; }

        [Required]
        [ForeignKey(nameof(ExerciseId))]
        public Exercise Exercise { get; set; } = null!;

        [Required]
        [Comment("Sets for the exercise")]
        public int Sets { get; set; }

        [Required]
        [Comment("Reps for the exercise")]
        public int Reps { get; set; }

        [Required]
        [Comment("Weight for the exercise")]
        public double Weight { get; set; }

        [Required]
        [Comment("Date Of Update")]
        [Column(TypeName = "date")]
        public DateTime DateOfUpdate { get; set; }
    }
}