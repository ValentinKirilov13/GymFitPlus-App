using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.SeedDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class ExserciseConfiguration : IEntityTypeConfiguration<Excercise>
    {
        public void Configure(EntityTypeBuilder<Excercise> builder)
        {
            builder
                .HasData(SeedData.SeedExsersise);
        }
    }
}
