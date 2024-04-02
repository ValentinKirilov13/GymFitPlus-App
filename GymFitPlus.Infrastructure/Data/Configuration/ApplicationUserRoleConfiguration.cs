using GymFitPlus.Infrastructure.Data.SeedDatabase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        private readonly SeedData _seedData;

        public ApplicationUserRoleConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder
                .HasData(new IdentityUserRole<Guid>[] { _seedData.MapAdminUserWithAdminRole });
        }
    }
}
