using GymFitPlus.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static GymFitPlus.Infrastructure.Constants.DataConstants.RecipeConstants;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table of recipes")]
    public class Recipe
    {
        [Key]
        [Comment("Recipe identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        [Comment("Recipe name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Comment("Calories in 100 grams of food")]
        public double CaloriesPerHundredGrams { get; set; }

        [Required]
        [Comment("Protein in 100 grams of food")]
        public double ProteinPerHundredGrams { get; set; }

        [Required]
        [Comment("Carbohidrates in 100 grams of food")]
        public double CarbsPerHundredGrams { get; set; }

        [Required]
        [Comment("Fat in 100 grams of food")]
        public double FatPerHundredGrams { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLenght)]
        [Comment("Recipe description about needed products and way of cooking")]
        public string Description { get; set; } = string.Empty;

        [Comment("Food image")]
        public byte[]? Image { get; set; }

        [Required]
        [Comment("Recipe category")]
        public RecipeType Category { get; set; }

        [Required]
        [Comment("Recipe status")]
        public bool IsDelete { get; set; } = false;
     
        public ICollection<UserRecipe> UsersRecipes { get; set; } = new List<UserRecipe>();
    }
}
