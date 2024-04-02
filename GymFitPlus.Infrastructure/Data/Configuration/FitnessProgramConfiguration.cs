using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.SeedDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class FitnessProgramConfiguration : IEntityTypeConfiguration<FitnessProgram>
    {
        private readonly SeedData _seedData;

        public FitnessProgramConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

        public void Configure(EntityTypeBuilder<FitnessProgram> builder)
        {
            builder
            .HasData(_seedData.FitnessPrograms);
        }
    }
}
