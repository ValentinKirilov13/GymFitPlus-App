using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GymFitPlus.Infrastructure.Constants.DataConstants.RecipeConstants;

namespace GymFitPlus.Infrastructure.Data.Models
{
    [Comment("Table of users and recipes")]
    public class UserRecipe
    {
        [Required]
        [Comment("User identifier")]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [Comment("Recipe identifier")]
        public int RecipeId { get; set; }

        [Required]
        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;

        [MaxLength(NoteMaxLenght)]
        [Comment("User note to current recipe")]
        public string? Note { get; set; }
    }
}
