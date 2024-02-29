using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class FitnessProgramExcerciseConfiguration : IEntityTypeConfiguration<FitnessProgramExcercise>
    {
        public void Configure(EntityTypeBuilder<FitnessProgramExcercise> builder)
        {
            builder
                .HasKey(pk => new { pk.FitnessProgramId, pk.ExcerciseId});
        }
    }
}
