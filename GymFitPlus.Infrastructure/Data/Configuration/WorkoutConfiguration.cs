using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.SeedDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{

    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        private readonly SeedData _seedData;

        public WorkoutConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder
                .HasOne(x => x.FitnessProgram)
                .WithMany(x => x.Workouts)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Workouts)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasData(_seedData.Workouts);
        }
    }
}
