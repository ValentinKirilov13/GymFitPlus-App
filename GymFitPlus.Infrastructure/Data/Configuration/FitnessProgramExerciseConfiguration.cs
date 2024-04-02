using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.SeedDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class FitnessProgramExerciseConfiguration : IEntityTypeConfiguration<FitnessProgramExercise>
    {
        private readonly SeedData _seedData;

        public FitnessProgramExerciseConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

        public void Configure(EntityTypeBuilder<FitnessProgramExercise> builder)
        {
            builder
                .HasKey(pk => new { pk.FitnessProgramId, pk.ExerciseId});

            builder
                .HasData(_seedData.FitnessProgramsExercise);
        }
    }
}
