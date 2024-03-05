using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class FitnessProgramExerciseConfiguration : IEntityTypeConfiguration<FitnessProgramExercise>
    {
        public void Configure(EntityTypeBuilder<FitnessProgramExercise> builder)
        {
            builder
                .HasKey(pk => new { pk.FitnessProgramId, pk.ExerciseId});
        }
    }
}
