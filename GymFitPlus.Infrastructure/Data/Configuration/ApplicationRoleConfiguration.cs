using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.SeedDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        private readonly SeedData _seedData;

        public ApplicationRoleConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder
                .HasData(new ApplicationRole[] { _seedData.AdminRole });
        }
    }
}
