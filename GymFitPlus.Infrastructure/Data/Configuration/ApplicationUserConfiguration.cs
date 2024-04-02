using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.SeedDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly SeedData _seedData;

        public ApplicationUserConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasData(new ApplicationUser[] { _seedData.FirstUser, _seedData.AdminUser });
        }
    }
}
