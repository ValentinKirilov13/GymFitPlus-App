using GymFitPlus.Infrastructure.Data.Configuration;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Infrastructure.Data
{
    public class ApplicationDbContext
      : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Excercise> Excercises { get; set; } = null!;
        public DbSet<FitnessProgram> FitnessPrograms { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FitnessProgramExcerciseConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
