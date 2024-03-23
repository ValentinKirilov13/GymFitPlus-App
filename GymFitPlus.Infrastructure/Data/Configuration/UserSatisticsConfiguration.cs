using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class UserSatisticsConfiguration : IEntityTypeConfiguration<UserSatistics>
    {
        public void Configure(EntityTypeBuilder<UserSatistics> builder)
        {
            
        }
    }
}
