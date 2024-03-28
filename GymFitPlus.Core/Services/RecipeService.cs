using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.RecipeViewModels;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Core.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository _repository;

        public RecipeService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RecipesAllViewModel>> AllRecipesAsync(AllRecipesQueryModel query)
        {
            var model = _repository
                .AllReadOnly<Recipe>()
                .Where(x => x.IsDelete == false)
                .Select(x => new RecipesAllViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Category = x.Category,
                    FavoriteByUsers = x.UsersRecipes.Count,
                    ProteinPerHundredGrams = x.ProteinPerHundredGrams,
                    FatPerHundredGrams = x.FatPerHundredGrams,
                    CarbsPerHundredGrams = x.CarbsPerHundredGrams
                });

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                string normalizedSearchTerm = query
                                                  .SearchTerm
                                                  .ToLower();
                model = model
                            .Where(m => m.Name
                                            .ToLower()
                                            .Contains(normalizedSearchTerm));
            }

            if (query.Category != default)
            {
                model = model
                            .Where(x => x.Category == query.Category);
            }

            model = query.Sorting switch
            {
                Sorting.Interactions => model
                                            .OrderByDescending(m => m.FavoriteByUsers),

                Sorting.Аlphabetical => model
                                            .OrderBy(m => m.Name),

                Sorting.Proteins => model
                                        .OrderByDescending(m => m.ProteinPerHundredGrams),

                Sorting.Fats => model
                                    .OrderByDescending(m => m.FatPerHundredGrams),

                Sorting.Carbohydrates => model
                                             .OrderByDescending(m => m.CarbsPerHundredGrams),

                _ => model
                         .OrderByDescending(m => m.Id)
            };

            query.TotalRecipesCount = model.Count();

            return await model
                            .Skip((query.CurrentPage - 1) * query.RecipesPerPage)
                            .Take(query.RecipesPerPage)
                            .ToListAsync();
        }

        public async Task<RecipeDetailsViewModel> FindRecipeByIdAsync(int id)
        {
            return await _repository
                 .AllReadOnly<Recipe>()
                 .Where(x => x.IsDelete == false)
                 .Select(x => new RecipeDetailsViewModel()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     ProteinPerHundredGrams = x.ProteinPerHundredGrams,
                     CarbsPerHundredGrams = x.CarbsPerHundredGrams,
                     FatPerHundredGrams = x.FatPerHundredGrams,
                     Image = x.Image,
                     Category = x.Category,
                     FavoriteByUsers = x.UsersRecipes.Count,
                 })
                 .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();
        }

        public async Task<bool> AddRecipeAsync(RecipeDetailsViewModel viewModel)
        {
            Recipe model = new Recipe()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                ProteinPerHundredGrams = viewModel.ProteinPerHundredGrams,
                CarbsPerHundredGrams = viewModel.CarbsPerHundredGrams,
                FatPerHundredGrams = viewModel.FatPerHundredGrams,
                Category = viewModel.Category,
            };

            if (viewModel.ImageForForm != null && viewModel.ImageForForm.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    viewModel.ImageForForm[0].CopyTo(memoryStream);
                    model.Image = memoryStream.ToArray();
                }
            }

            await _repository.AddAsync(model);

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var model = await FindByIdAsync(id);

            model.IsDelete = true;

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<bool> EditRecipeAsync(RecipeDetailsViewModel viewModel)
        {
            var model = await FindByIdAsync(viewModel.Id);

            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.ProteinPerHundredGrams = viewModel.ProteinPerHundredGrams;
            model.CarbsPerHundredGrams = viewModel.CarbsPerHundredGrams;
            model.FatPerHundredGrams = viewModel.FatPerHundredGrams;
            model.Category = viewModel.Category;

            if (viewModel.ImageForForm != null && viewModel.ImageForForm.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    viewModel.ImageForForm[0].CopyTo(memoryStream);
                    model.Image = memoryStream.ToArray();
                }
            }

            int affectedRows = await _repository.SaveChangesAsync();

            return affectedRows > 0;
        }

        private async Task<Recipe> FindByIdAsync(int id)
        {
            return await _repository
                .All<Recipe>()
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NullReferenceException();
        }
    }
}
