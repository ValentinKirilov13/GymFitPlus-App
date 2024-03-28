﻿using GymFitPlus.Infrastructure.Data.Configuration;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Infrastructure.Data
{
    public class ApplicationDbContext
      : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Exercise> Exercises { get; set; } = null!;
        public DbSet<FitnessProgram> FitnessPrograms { get; set; } = null!;
        public DbSet<Workout> Workouts { get; set; } = null!;
        public DbSet<UserSatistics> UserSatistics { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<UserRecipe> UsersRecipes { get; set; } = null!;
        public DbSet<FitnessProgramExercise> FitnessProgramsExercises { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FitnessProgramExerciseConfiguration());
            builder.ApplyConfiguration(new ExerciseConfiguration());
            builder.ApplyConfiguration(new WorkoutConfiguration());
            builder.ApplyConfiguration(new FitnessProgramConfiguration());
            builder.ApplyConfiguration(new UserSatisticsConfiguration());
            builder.ApplyConfiguration(new RecipeConfiguration());
            builder.ApplyConfiguration(new UserRecipeConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
