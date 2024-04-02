using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.SeedDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class UserStatisticsConfiguration : IEntityTypeConfiguration<UserStatistics>
    {
        private readonly SeedData _seedData;

        public UserStatisticsConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

        public void Configure(EntityTypeBuilder<UserStatistics> builder)
        {
            builder
                .HasData(_seedData.UserStatistics);
        }
    }
}
