using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.Services;
using GymFitPlus.Core.ViewModels.RecipeViewModels;
using GymFitPlus.Infrastructure.Data;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.UnitTests
{
    [TestFixture]
    public class RecipeServiceTests
    {
        private IRecipeService _recipeService;
        private IRepository _repository;
        private ApplicationDbContext _applicationDbContext;
        private List<Recipe> _recipes;
        private List<UserRecipe> _usersRecipes;
        private Guid _firstUserId;
        private Guid _secondUserId;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "RecipesDB")
               .Options;

            _applicationDbContext = new ApplicationDbContext(contextOptions);
            _repository = new Repository(_applicationDbContext);

            _recipes = new List<Recipe>
            {
                new Recipe
                {
                    Id = 1,
                    Name = "Omelette",
                    CaloriesPerHundredGrams = 154,
                    ProteinPerHundredGrams = 13.1,
                    CarbsPerHundredGrams = 1.1,
                    FatPerHundredGrams = 11.1,
                    Description = "Delicious omelette with veggies and cheese.",
                    Category = RecipeType.Salty,
                },
                new Recipe
                {
                    Id = 2,
                    Name = "Grilled Chicken Salad",
                    CaloriesPerHundredGrams = 120,
                    ProteinPerHundredGrams = 15.5,
                    CarbsPerHundredGrams = 2.5,
                    FatPerHundredGrams = 5.0,
                    Description = "Healthy salad with grilled chicken, lettuce, tomatoes, and cucumbers.",
                    Category = RecipeType.Salty,
                },
                new Recipe
                {
                    Id = 3,
                    Name = "Spaghetti Bolognese",
                    CaloriesPerHundredGrams = 200,
                    ProteinPerHundredGrams = 12.3,
                    CarbsPerHundredGrams = 20.5,
                    FatPerHundredGrams = 8.7,
                    Description = "Classic Italian pasta dish with a rich meat sauce.",
                    Category = RecipeType.Salty,
                },
                new Recipe
                {
                    Id = 4,
                    Name = "Fruit Smoothie",
                    CaloriesPerHundredGrams = 80,
                    ProteinPerHundredGrams = 2.0,
                    CarbsPerHundredGrams = 18.0,
                    FatPerHundredGrams = 0.5,
                    Description = "Refreshing smoothie made with mixed fruits and yogurt.",
                    Category = RecipeType.Sweety,
                },
                new Recipe
                {
                    Id = 5,
                    Name = "Chocolate Cake",
                    CaloriesPerHundredGrams = 350,
                    ProteinPerHundredGrams = 5.5,
                    CarbsPerHundredGrams = 50.0,
                    FatPerHundredGrams = 15.0,
                    Description = "Decadent chocolate cake topped with ganache.",
                    Category = RecipeType.Sweety,
                }
            };

            _firstUserId = Guid.Parse("763dc2ab-eea2-4f09-9aa7-a678db581377");
            _secondUserId = Guid.Parse("647d2cc2-5c4c-4eb1-808b-38b4d0433c36");

            _usersRecipes = new List<UserRecipe>
            {
                new UserRecipe
                {
                    UserId = _firstUserId,
                    RecipeId = 1,
                    Note = "This recipe turned out really well! I added some extra spices for flavor."
                },
                new UserRecipe
                {
                    UserId = _firstUserId,
                    RecipeId = 2,
                },
                new UserRecipe
                {
                    UserId = _firstUserId,
                    RecipeId = 3,
                    Note = "Make sure to simmer the sauce for at least 30 minutes for best flavor."
                },
                new UserRecipe
                {
                    UserId = _secondUserId,
                    RecipeId = 4,
                    Note = "I prefer using frozen berries for a thicker consistency."
                },
                new UserRecipe
                {
                    UserId = _secondUserId,
                    RecipeId = 5,
                }
            };

            _applicationDbContext.AddRange(_recipes);
            _applicationDbContext.AddRange(_usersRecipes);
            _applicationDbContext.SaveChanges();

            _recipeService = new RecipeService(_repository);
        }

        [TearDown]
        public void TearDown()
        {
            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Dispose();
        }

        [TestCase(1, 3)]
        [TestCase(2, 2)]
        public async Task AllRecipesAsync_ShouldReturnAllForEachPageNotFavourite(int currPage, int expectedResultCount)
        {
            var query = new AllRecipesQueryModel()
            {
                CurrentPage = currPage
            };

            IEnumerable<RecipesAllViewModel> result =
                await _recipeService.AllRecipesAsync(query, favourite: false, Guid.NewGuid());

            int allRecipesInDb = await _applicationDbContext.Recipes.Where(x => x.IsDelete == false).CountAsync();

            IEnumerable<RecipesAllViewModel> sortedCollection =
                 await _applicationDbContext.Recipes
                .Where(x => x.IsDelete == false)
                .Select(x => new RecipesAllViewModel()
                {
                    Id = x.Id
                })
                .OrderByDescending(x => x.Id)
                .Skip((query.CurrentPage - 1) * query.RecipesPerPage)
                .Take(query.RecipesPerPage)
                .ToListAsync();

            Assert.That(result.Count(), Is.EqualTo(expectedResultCount));
            Assert.That(query.TotalRecipesCount, Is.EqualTo(allRecipesInDb));
            Assert.That(query.IsFavourite, Is.False);
            Assert.That(result.First().Id, Is.EqualTo(sortedCollection.First().Id));
            Assert.That(result.Last().Id, Is.EqualTo(sortedCollection.Last().Id));
        }

        [TestCase(Sorting.Interactions)]
        [TestCase(Sorting.Аlphabetical)]
        [TestCase(Sorting.Proteins)]
        [TestCase(Sorting.Calories)]
        [TestCase(Sorting.Carbohydrates)]
        [TestCase(Sorting.Fats)]
        public async Task AllRecipesAsync_ShouldSort(Sorting sorting)
        {
            var query = new AllRecipesQueryModel()
            {
                Sorting = sorting
            };

            IEnumerable<RecipesAllViewModel> result =
                await _recipeService.AllRecipesAsync(query, favourite: false, Guid.NewGuid());

            IQueryable<RecipesAllViewModel> sortedCollection =
                _applicationDbContext.Recipes
               .Where(x => x.IsDelete == false)
               .Select(x => new RecipesAllViewModel()
               {
                   Id = x.Id,
                   Name = x.Name,
                   FavoriteByUsers = x.UsersRecipes.Count,
                   CaloriesPerHundredGrams = x.CaloriesPerHundredGrams,
                   ProteinPerHundredGrams = x.ProteinPerHundredGrams,
                   FatPerHundredGrams = x.FatPerHundredGrams,
                   CarbsPerHundredGrams = x.CarbsPerHundredGrams
               });

            if (sorting == Sorting.Interactions)
            {
                sortedCollection = sortedCollection.OrderByDescending(m => m.FavoriteByUsers);
            }
            else if (sorting == Sorting.Аlphabetical)
            {
                sortedCollection = sortedCollection.OrderBy(x => x.Name);
            }
            else if (sorting == Sorting.Proteins)
            {
                sortedCollection = sortedCollection.OrderByDescending(m => m.ProteinPerHundredGrams);
            }
            else if (sorting == Sorting.Fats)
            {
                sortedCollection = sortedCollection.OrderByDescending(m => m.FatPerHundredGrams);
            }
            else if (sorting == Sorting.Calories)
            {
                sortedCollection = sortedCollection.OrderByDescending(m => m.CaloriesPerHundredGrams);
            }
            else if (sorting == Sorting.Carbohydrates)
            {
                sortedCollection = sortedCollection.OrderByDescending(m => m.CarbsPerHundredGrams);
            }

            IEnumerable<RecipesAllViewModel> actual = await sortedCollection
               .Skip((query.CurrentPage - 1) * query.RecipesPerPage)
               .Take(query.RecipesPerPage)
               .ToListAsync();

            Assert.That(result.First().Id, Is.EqualTo(actual.First().Id));
            Assert.That(result.Last().Id, Is.EqualTo(actual.Last().Id));
        }

        [TestCase("Ome")]
        [TestCase("fruit")]
        [TestCase("Grill")]
        public async Task AllRecipesAsync_ShouldSearchTermReturn(string searchTerm)
        {
            var query = new AllRecipesQueryModel()
            {
                SearchTerm = searchTerm
            };

            IEnumerable<RecipesAllViewModel> result =
               await _recipeService.AllRecipesAsync(query, favourite: false, Guid.NewGuid());

            IEnumerable<RecipesAllViewModel> sortedCollection =
                await _applicationDbContext.Recipes
               .Where(x => x.IsDelete == false && x.Name.ToLower().Contains(searchTerm.ToLower()))
               .Select(x => new RecipesAllViewModel()
               {
                   Id = x.Id,
               })
               .OrderByDescending(x => x.Id)
               .Skip((query.CurrentPage - 1) * query.RecipesPerPage)
               .Take(query.RecipesPerPage)
               .ToListAsync();

            Assert.That(result.First().Id, Is.EqualTo(sortedCollection.First().Id));
            Assert.That(result.Last().Id, Is.EqualTo(sortedCollection.Last().Id));
        }

        [TestCase(RecipeType.Sweety)]
        [TestCase(RecipeType.Salty)]
        public async Task AllRecipesAsync_ShouldCategoryReturn(RecipeType category)
        {
            var query = new AllRecipesQueryModel()
            {
                Category = category
            };

            IEnumerable<RecipesAllViewModel> result =
               await _recipeService.AllRecipesAsync(query, favourite: false, Guid.NewGuid());

            IEnumerable<RecipesAllViewModel> sortedCollection =
                 await _applicationDbContext.Recipes
                .Where(x => x.IsDelete == false && x.Category == category)
                .Select(x => new RecipesAllViewModel()
                {
                    Id = x.Id,
                })
                .OrderByDescending(x => x.Id)
                .Skip((query.CurrentPage - 1) * query.RecipesPerPage)
                .Take(query.RecipesPerPage)
                .ToListAsync();

            Assert.That(result.First().Id, Is.EqualTo(sortedCollection.First().Id));
            Assert.That(result.Last().Id, Is.EqualTo(sortedCollection.Last().Id));
        }

        [TestCase(3, "763dc2ab-eea2-4f09-9aa7-a678db581377")]
        [TestCase(2, "647d2cc2-5c4c-4eb1-808b-38b4d0433c36")]
        public async Task AllRecipesAsync_ShouldReturnAllForEachPageFavourite(int expectedResultCount, string userId)
        {
            var query = new AllRecipesQueryModel();

            IEnumerable<RecipesAllViewModel> result =
                await _recipeService.AllRecipesAsync(query, favourite: true, Guid.Parse(userId));

            IEnumerable<RecipesAllViewModel> sortedCollection =
                 await _applicationDbContext.UsersRecipes
                .Where(x => x.Recipe.IsDelete == false && x.UserId == Guid.Parse(userId))
                .Select(x => new RecipesAllViewModel()
                {
                    Id = x.Recipe.Id
                })
                .OrderByDescending(x => x.Id)
                .Skip((query.CurrentPage - 1) * query.RecipesPerPage)
                .Take(query.RecipesPerPage)
                .ToListAsync();

            Assert.That(result.Count(), Is.EqualTo(expectedResultCount));
            Assert.That(result.First().Id, Is.EqualTo(sortedCollection.First().Id));
            Assert.That(result.Last().Id, Is.EqualTo(sortedCollection.Last().Id));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(1)]
        public async Task FindRecipeByIdAsync_ShouldReturnConcreteRecipeById(int id)
        {
            var returned = await _recipeService.FindRecipeByIdAsync(id, favourite: false, Guid.NewGuid());

            var expected = await _applicationDbContext
                .Recipes
                .Where(x => x.IsDelete == false)
                .Select(x => new RecipeDetailsViewModel()
                {
                    Id = x.Id,
                })
                .FirstAsync(x => x.Id == id);

            Assert.That(returned, Is.Not.Null);
            Assert.That(returned.Note, Is.Null);
            Assert.That(returned.Id, Is.EqualTo(expected.Id));
        }

        [Test]
        public void FindRecipeByIdAsync_ShouldThrowNullReferenceException()
        {
            int id = 8;

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _recipeService.FindRecipeByIdAsync(id, favourite: false, Guid.NewGuid()));
        }

        [TestCase(1, "763dc2ab-eea2-4f09-9aa7-a678db581377")]
        [TestCase(4, "647d2cc2-5c4c-4eb1-808b-38b4d0433c36")]
        public async Task FindRecipeByIdAsync_ShouldReturnConcreteFavouriteRecipeById(int id, string userId)
        {
            var returned = await _recipeService.FindRecipeByIdAsync(id, favourite: true, Guid.Parse(userId));

            var expected = await _applicationDbContext
                .Recipes
                .Where(x => x.IsDelete == false)
                .Select(x => new RecipeDetailsViewModel()
                {
                    Id = x.Id,
                    Note = x.UsersRecipes
                             .Where(ur => ur.UserId == Guid.Parse(userId) && ur.RecipeId == x.Id)
                             .Select(ur => ur.Note)
                             .FirstOrDefault()
                })
                .FirstAsync(x => x.Id == id);

            Assert.That(returned, Is.Not.Null);
            Assert.That(returned.Note, Is.Not.Null);
            Assert.That(returned.Id, Is.EqualTo(expected.Id));
            Assert.That(returned.Note, Is.EqualTo(expected.Note));
        }

        [Test]
        public async Task AddRecipeAsync_ShouldAddRecipeInDatabaseWithImg()
        {
            byte[] fileBytes = File.ReadAllBytes(@"C:\Users\valki\Desktop\Programing\SoftUni Project For Defense\GymFitPlus\GymFitPlus.Web\wwwroot\images\Avatar\avatar.svg");

            MemoryStream stream = new MemoryStream(fileBytes);

            var viewModel = new RecipeDetailsViewModel
            {
                Name = "Custom recipe",
                Description = "Very delicious",
                CaloriesPerHundredGrams = 300,
                ProteinPerHundredGrams = 20,
                CarbsPerHundredGrams = 15,
                FatPerHundredGrams = 2,
                Category = RecipeType.Salty,
                ImageForForm = new FormFile[] { new FormFile(stream, 0, fileBytes.Length, "fileName", "fileName") }
            };

            bool succeed = await _recipeService.AddRecipeAsync(viewModel);

            int expectedRecipesInDb = 6;
            int actualRecipesInDb = await _applicationDbContext.Recipes.CountAsync();
            var insertedRecipe = await _applicationDbContext.Recipes.FindAsync(6);

            Assert.That(succeed, Is.True);
            Assert.That(actualRecipesInDb, Is.EqualTo(expectedRecipesInDb));
            Assert.That(insertedRecipe, Is.Not.Null);
            Assert.That(insertedRecipe.Image, Is.Not.Null);
        }

        [Test]
        public async Task AddRecipeAsync_ShouldAddRecipeInDatabaseWithoutImg()
        {
            var viewModel = new RecipeDetailsViewModel
            {
                Name = "Custom recipe",
                Description = "Very delicious",
                CaloriesPerHundredGrams = 300,
                ProteinPerHundredGrams = 20,
                CarbsPerHundredGrams = 15,
                FatPerHundredGrams = 2,
                Category = RecipeType.Salty,
            };

            bool succeed = await _recipeService.AddRecipeAsync(viewModel);

            int expectedRecipesInDb = 6;
            int actualRecipesInDb = await _applicationDbContext.Recipes.CountAsync();
            var insertedRecipe = await _applicationDbContext.Recipes.FindAsync(6);

            Assert.That(succeed, Is.True);
            Assert.That(actualRecipesInDb, Is.EqualTo(expectedRecipesInDb));
            Assert.That(insertedRecipe, Is.Not.Null);
            Assert.That(insertedRecipe.Image, Is.Null);
        }

        [Test]
        public async Task DeleteRecipeAsync_ShouldMakePropertyIsDeleteTrue()
        {
            int id = 2;

            bool succeed = await _recipeService.DeleteRecipeAsync(id);

            var deletedRecipe = await _applicationDbContext.Recipes.FindAsync(id);

            Assert.That(succeed, Is.True);
            Assert.That(deletedRecipe, Is.Not.Null);
            Assert.That(deletedRecipe.IsDelete, Is.True);
        }

        [Test]
        public void DeleteRecipeAsync_ShouldThrowNullReferenceException()
        {
            int id = 8;

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _recipeService.DeleteRecipeAsync(id));
        }

        [Test]
        public async Task EditRecipeAsync_ShouldGetAndEditRecipeInDatabaseWithImg()
        {
            byte[] fileBytes = File.ReadAllBytes(@"C:\Users\valki\Desktop\Programing\SoftUni Project For Defense\GymFitPlus\GymFitPlus.Web\wwwroot\images\Avatar\avatar.svg");
            MemoryStream stream = new MemoryStream(fileBytes);

            int id = 2;
            string newName = "I don't know";

            var viewModel = new RecipeDetailsViewModel
            {
                Id = id,
                Name = newName,
                Description = "Very delicious",
                CaloriesPerHundredGrams = 300,
                ProteinPerHundredGrams = 20,
                CarbsPerHundredGrams = 15,
                FatPerHundredGrams = 2,
                Category = RecipeType.Salty,
                ImageForForm = new FormFile[] { new FormFile(stream, 0, fileBytes.Length, "fileName", "fileName") }
            };

            bool succeed = await _recipeService.EditRecipeAsync(viewModel);

            int expectedRecipesInDb = 5;
            int actualRecipesInDb = await _applicationDbContext.Recipes.CountAsync();
            var editedRecipe = await _applicationDbContext.Recipes.FindAsync(id);

            Assert.That(succeed, Is.True);
            Assert.That(actualRecipesInDb, Is.EqualTo(expectedRecipesInDb));
            Assert.That(editedRecipe, Is.Not.Null);
            Assert.That(editedRecipe.Name, Is.EqualTo(viewModel.Name));
            Assert.That(editedRecipe.Description, Is.EqualTo(viewModel.Description));
            Assert.That(editedRecipe.CaloriesPerHundredGrams, Is.EqualTo(viewModel.CaloriesPerHundredGrams));
            Assert.That(editedRecipe.ProteinPerHundredGrams, Is.EqualTo(viewModel.ProteinPerHundredGrams));
            Assert.That(editedRecipe.CarbsPerHundredGrams, Is.EqualTo(viewModel.CarbsPerHundredGrams));
            Assert.That(editedRecipe.FatPerHundredGrams, Is.EqualTo(viewModel.FatPerHundredGrams));
            Assert.That(editedRecipe.Category, Is.EqualTo(viewModel.Category));
            Assert.That(editedRecipe.Image, Is.Not.Null);
            Assert.That(editedRecipe.Image, Is.EqualTo(fileBytes));
        }

        [Test]
        public void EditRecipeAsync_ShouldThrowNullReferenceException()
        {
            var viewModel = new RecipeDetailsViewModel() { Id = 8, };

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _recipeService.EditRecipeAsync(viewModel));
        }

        [Test]
        public async Task EditRecipeAsync_ShouldGetAndEditRecipeInDatabaseWithoutImg()
        {
            int id = 2;
            string newName = "I don't know";

            var viewModel = new RecipeDetailsViewModel
            {
                Id = id,
                Name = newName,
                Description = "Very delicious",
                CaloriesPerHundredGrams = 300,
                ProteinPerHundredGrams = 20,
                CarbsPerHundredGrams = 15,
                FatPerHundredGrams = 2,
                Category = RecipeType.Salty,
            };

            bool succeed = await _recipeService.EditRecipeAsync(viewModel);

            int expectedRecipesInDb = 5;
            int actualRecipesInDb = await _applicationDbContext.Recipes.CountAsync();
            var editedRecipe = await _applicationDbContext.Recipes.FindAsync(id);

            Assert.That(succeed, Is.True);
            Assert.That(actualRecipesInDb, Is.EqualTo(expectedRecipesInDb));
            Assert.That(editedRecipe, Is.Not.Null);
            Assert.That(editedRecipe.Name, Is.EqualTo(viewModel.Name));
            Assert.That(editedRecipe.Description, Is.EqualTo(viewModel.Description));
            Assert.That(editedRecipe.CaloriesPerHundredGrams, Is.EqualTo(viewModel.CaloriesPerHundredGrams));
            Assert.That(editedRecipe.ProteinPerHundredGrams, Is.EqualTo(viewModel.ProteinPerHundredGrams));
            Assert.That(editedRecipe.CarbsPerHundredGrams, Is.EqualTo(viewModel.CarbsPerHundredGrams));
            Assert.That(editedRecipe.FatPerHundredGrams, Is.EqualTo(viewModel.FatPerHundredGrams));
            Assert.That(editedRecipe.Category, Is.EqualTo(viewModel.Category));
            Assert.That(editedRecipe.Image, Is.Null);
        }

        [Test]
        public async Task AddRecipeToFavouriteAsync_ShouldAddConcreteRecipeToConcreteUserFavouriteList()
        {
            var viewModel = new RecipeDetailsViewModel() { Id = 2, };
            var secondUser = Guid.Parse("647d2cc2-5c4c-4eb1-808b-38b4d0433c36");

            bool succeed = await _recipeService.AddRecipeToFavouriteAsync(viewModel, secondUser);

            int expectedCountInUserRecipeTable = 6;
            int returnedCount = await _applicationDbContext.UsersRecipes.CountAsync();
            var userRecipe = await _applicationDbContext
                .UsersRecipes
                .FirstOrDefaultAsync(x => x.RecipeId == viewModel.Id && x.UserId == secondUser);

            Assert.That(succeed, Is.True);
            Assert.That(expectedCountInUserRecipeTable, Is.EqualTo(returnedCount));
            Assert.That(userRecipe, Is.Not.Null);
        }

        [Test]
        public async Task AddRecipeToFavouriteAsync_ShouldNotAddAllreadyExistFavouriteRelation()
        {
            var viewModel = new RecipeDetailsViewModel() { Id = 5, };
            var secondUser = Guid.Parse("647d2cc2-5c4c-4eb1-808b-38b4d0433c36");

            bool succeed = await _recipeService.AddRecipeToFavouriteAsync(viewModel, secondUser);

            int expectedCountInUserRecipeTable = 5;
            int returnedCount = await _applicationDbContext.UsersRecipes.CountAsync();
            var userRecipe = await _applicationDbContext
                .UsersRecipes
                .FirstOrDefaultAsync(x => x.RecipeId == viewModel.Id && x.UserId == secondUser);

            Assert.That(succeed, Is.False);
            Assert.That(expectedCountInUserRecipeTable, Is.EqualTo(returnedCount));
            Assert.That(userRecipe, Is.Not.Null);
        }

        [Test]
        public void AddRecipeToFavouriteAsync_ShouldThrowNullReferenceException()
        {
            var viewModel = new RecipeDetailsViewModel() { Id = 10, };

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _recipeService.AddRecipeToFavouriteAsync(viewModel, Guid.NewGuid()));
        }

        [TestCase(1, "763dc2ab-eea2-4f09-9aa7-a678db581377")]
        [TestCase(2, "763dc2ab-eea2-4f09-9aa7-a678db581377")]
        [TestCase(5, "647d2cc2-5c4c-4eb1-808b-38b4d0433c36")]
        [TestCase(4, "647d2cc2-5c4c-4eb1-808b-38b4d0433c36")]
        public async Task EditFavouriteRecipeAsync_ShouldEditNoteInFavouriteRecipe(int recipeId, string userId)
        {
            var viewModel = new RecipeDetailsViewModel() { Id = recipeId, Note = "New Note" };
            var user = Guid.Parse(userId);

            bool succeed = await _recipeService.EditFavouriteRecipeAsync(viewModel, user);

            var userRecipe = await _applicationDbContext
                .UsersRecipes
                .AsNoTracking()
                .FirstAsync(x => x.RecipeId == viewModel.Id && x.UserId == user);

            Assert.That(succeed, Is.True);
            Assert.That(userRecipe.Note, Is.EqualTo(viewModel.Note));
        }

        [Test]
        public void EditFavouriteRecipeAsync_ShouldThrowNullReferenceException()
        {
            var viewModel = new RecipeDetailsViewModel() { Id = 8, };

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _recipeService.EditFavouriteRecipeAsync(viewModel, Guid.NewGuid()));
        }

        [TestCase(1, "763dc2ab-eea2-4f09-9aa7-a678db581377")]
        [TestCase(2, "763dc2ab-eea2-4f09-9aa7-a678db581377")]
        [TestCase(5, "647d2cc2-5c4c-4eb1-808b-38b4d0433c36")]
        [TestCase(4, "647d2cc2-5c4c-4eb1-808b-38b4d0433c36")]
        public async Task DeleteRecipeFromFavouriteAsync_ShouldDeleteFavouriteRecipe(int recipeId, string userId)
        {
            var viewModel = new RecipeDetailsViewModel() { Id = recipeId };
            var user = Guid.Parse(userId);

            var existingEntity = _applicationDbContext.Set<UserRecipe>().Local
                .FirstOrDefault(e => e.RecipeId == recipeId && e.UserId == user);

            if (existingEntity != null)
            {
                _applicationDbContext.Entry(existingEntity).State = EntityState.Detached;
            }

            bool succeed = await _recipeService.DeleteRecipeFromFavouriteAsync(viewModel, user);

            var userRecipe = await _applicationDbContext
                .UsersRecipes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.RecipeId == viewModel.Id && x.UserId == user);

            Assert.That(succeed, Is.True);
            Assert.That(userRecipe, Is.Null);
        }

        [Test]
        public void DeleteRecipeFromFavouriteAsync_ShouldThrowNullReferenceException()
        {
            var viewModel = new RecipeDetailsViewModel() { Id = 8, };

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _recipeService.DeleteRecipeFromFavouriteAsync(viewModel, Guid.NewGuid()));
        }

        [TestCase(false, 4)]
        [TestCase(true, 1)]
        public async Task AllRecipeForAdminAsync_ShouldReturnAllActiveOrDeletedRecipes(bool deleted, int expectedCount)
        {
            await _recipeService.DeleteRecipeAsync(2);

            var returnedRecipes = await _recipeService.AllRecipeForAdminAsync(deleted);
            int returnedCount = returnedRecipes.Count();

            Assert.That(returnedCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task RestoreRecipeAsync_ShouldMakeIsDeleteFalse()
        {
            int recipeId = 2;
            await _recipeService.DeleteRecipeAsync(recipeId);

            int expectedBeforCount = 4;
            int beforeAllActiveRecipes = await _applicationDbContext
                .Recipes
                .Where(x => x.IsDelete == false)
                .CountAsync();

            bool succeed = await _recipeService.RestoreRecipeAsync(recipeId);

            int expectedAfter = 5;
            int afterAllActiveRecipes = await _applicationDbContext
                .Recipes
                .Where(x => x.IsDelete == false)
                .CountAsync();

            var returnedRecipes = await _applicationDbContext
                .Recipes
                .Where(x => x.IsDelete == false)
                .FirstOrDefaultAsync(x => x.Id == recipeId);

            Assert.That(beforeAllActiveRecipes, Is.EqualTo(expectedBeforCount));
            Assert.That(succeed, Is.True);
            Assert.That(afterAllActiveRecipes, Is.EqualTo(expectedAfter));
            Assert.That(returnedRecipes, Is.Not.Null);
            Assert.That(returnedRecipes.IsDelete, Is.False);
        }

        [Test]
        public void RestoreRecipeAsync_ShouldThrowNullReferenceException()
        {
            int recipeId = 2;

            Assert.ThrowsAsync<NullReferenceException>(async ()
                 => await _recipeService.RestoreRecipeAsync(recipeId));
        }
    }
}
