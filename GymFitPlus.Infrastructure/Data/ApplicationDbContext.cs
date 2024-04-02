using GymFitPlus.Infrastructure.Data.Configuration;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.SeedDatabase;
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
        public DbSet<UserStatistics> UserStatistics { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<UserRecipe> UsersRecipes { get; set; } = null!;
        public DbSet<FitnessProgramExercise> FitnessProgramsExercises { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedData dataForSeed = new SeedData();

            builder.ApplyConfiguration(new ApplicationUserConfiguration(dataForSeed));
            builder.ApplyConfiguration(new ApplicationRoleConfiguration(dataForSeed));
            builder.ApplyConfiguration(new ApplicationUserRoleConfiguration(dataForSeed));

            builder.ApplyConfiguration(new ExerciseConfiguration(dataForSeed));
            builder.ApplyConfiguration(new RecipeConfiguration(dataForSeed));

            builder.ApplyConfiguration(new FitnessProgramConfiguration(dataForSeed));
            builder.ApplyConfiguration(new FitnessProgramExerciseConfiguration(dataForSeed));
            builder.ApplyConfiguration(new UserRecipeConfiguration(dataForSeed));
            builder.ApplyConfiguration(new UserStatisticsConfiguration(dataForSeed));
            builder.ApplyConfiguration(new WorkoutConfiguration(dataForSeed));

            base.OnModelCreating(builder);
        }
    }
}
