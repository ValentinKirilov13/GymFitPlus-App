using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class FitnessProgramConfiguration : IEntityTypeConfiguration<FitnessProgram>
    {
        public void Configure(EntityTypeBuilder<FitnessProgram> builder)
        {
                           
        }
    }
}
