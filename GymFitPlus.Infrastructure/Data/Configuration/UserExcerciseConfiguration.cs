using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class UserExcerciseConfiguration : IEntityTypeConfiguration<UserExcercise>
    {
        public void Configure(EntityTypeBuilder<UserExcercise> builder)
        {
            builder
                .HasKey(pk => new { pk.ExcerciseId, pk.UserId });
        }
    }
}
