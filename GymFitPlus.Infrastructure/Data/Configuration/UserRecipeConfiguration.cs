using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Data.SeedDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymFitPlus.Infrastructure.Data.Configuration
{
    public class UserRecipeConfiguration : IEntityTypeConfiguration<UserRecipe>
    {
        private readonly SeedData _seedData;
        public UserRecipeConfiguration(SeedData seedData)
        {
            _seedData = seedData;
        }

        public void Configure(EntityTypeBuilder<UserRecipe> builder)
        {
            builder
                .HasKey(pk => new { pk.UserId, pk.RecipeId });

            builder
                .HasData(_seedData.UserRecipes);
        }
    }
}
