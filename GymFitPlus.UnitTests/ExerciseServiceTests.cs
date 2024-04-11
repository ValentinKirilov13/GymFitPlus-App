using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.Services;
using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using GymFitPlus.Infrastructure.Data;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GymFitPlus.UnitTests
{
    [TestFixture]
    public class ExerciseServiceTests
    {
        private IExerciseService _exerciseService;
        private IRepository _repository;
        private ApplicationDbContext _applicationDbContext;
        private List<Exercise> _exerciseList;
        private List<FitnessProgram> _fitnessProgram;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "ExerciseDB")
               .Options;

            _applicationDbContext = new ApplicationDbContext(contextOptions);
            _repository = new Repository(_applicationDbContext);
            var mockFitnessProgramService = new Mock<IFitnessProgramService>();
            mockFitnessProgramService
                .Setup(x => x.GetAllExerciseFromProgramAsync(3))
                .ReturnsAsync(new List<int> { 2, 3 });

            _exerciseList = new List<Exercise>
            {
                new Exercise
                {
                    Id = 1,
                    Name = "Squat",
                    Description = "The squat is a compound exercise that primarily targets the muscles of the thighs, hips, and             buttocks.",
                    VideoUrl = "https://www.example.com/squat_video",
                    MuscleGroup = MuscleGroup.Legs,
                    FitnessProgramsExercises = new List<FitnessProgramExercise>()
                    {
                        new FitnessProgramExercise()
                        {
                            ExerciseId = 1,
                            FitnessProgramId = 1,
                        },
                    }
                },
                new Exercise
                {
                    Id = 2,
                    Name = "Push-up",
                    Description = "The push-up is a basic exercise that targets the muscles of the chest, shoulders, and    triceps.",
                    VideoUrl = "https://www.example.com/push_up_video",
                    MuscleGroup = MuscleGroup.Chest,
                    FitnessProgramsExercises = new List<FitnessProgramExercise>()
                    {
                        new FitnessProgramExercise()
                        {
                            ExerciseId = 2,
                            FitnessProgramId = 1,
                        },
                        new FitnessProgramExercise()
                        {
                            ExerciseId = 2,
                            FitnessProgramId = 2,
                        },
                        new FitnessProgramExercise()
                        {
                            ExerciseId = 2,
                            FitnessProgramId = 3,
                        },
                    }
                },
                new Exercise
                {
                    Id = 3,
                    Name = "Pull-up",
                    Description = "The pull-up is a strength training exercise that targets the upper body muscles including the        back,  shoulders, and arms.",
                    VideoUrl = "https://www.example.com/pull_up_video",
                    MuscleGroup = MuscleGroup.Chest,
                     FitnessProgramsExercises = new List<FitnessProgramExercise>()
                    {
                        new FitnessProgramExercise()
                        {
                            ExerciseId = 3,
                            FitnessProgramId = 1,
                        },
                         new FitnessProgramExercise()
                        {
                            ExerciseId = 3,
                            FitnessProgramId = 3,
                        },
                    }
                }
            };
            _fitnessProgram = new List<FitnessProgram>
            {
                new FitnessProgram
                {
                    Id = 1,
                    Name = "Weight Loss Program",
                    UserId = Guid.NewGuid(),
                    IsDelete = false,
                },
                new FitnessProgram
                {
                    Id = 2,
                    Name = "Strength Training Program",
                    UserId = Guid.NewGuid(),
                },
                new FitnessProgram
                {
                    Id = 3,
                    Name = "Cardiovascular Program",
                    UserId = Guid.NewGuid(),
                }
            };

            _applicationDbContext.AddRange(_fitnessProgram);
            _applicationDbContext.AddRange(_exerciseList);
            _applicationDbContext.SaveChanges();

            _exerciseService = new ExerciseService(_repository, mockFitnessProgramService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Dispose();
        }

        [Test]
        public async Task AddExerciseAsync_ShouldAddExerciseInDatabaseAsync()
        {
            var viewModel = new ExerciseDetailViewModel
            {
                Name = "Deadlift",
                Description = "The deadlift is a weight training exercise in which a loaded barbell or bar is lifted off the ground to the level of the hips, then lowered back to the ground.",
                VideoUrl = "https://www.youtube.com/watch?v=op9kVnSso6Q",
                MuscleGroup = MuscleGroup.Chest
            };

            bool succeed = await _exerciseService.AddExerciseAsync(viewModel);

            int expectedExerciseInDb = 4;
            int actualExerciseInDb = await _applicationDbContext.Exercises.CountAsync();
            var insertedExercise = await _applicationDbContext.Exercises.FindAsync(4);

            Assert.That(succeed);
            Assert.That(actualExerciseInDb, Is.EqualTo(expectedExerciseInDb));
            Assert.That(insertedExercise, Is.Not.Null);
        }

        [Test]
        public async Task EditExerciseAsync_ShouldGetAndEditExerciseInDatabaseAsync()
        {
            int id = 2;
            var exerciseToEdit = await _applicationDbContext.Exercises.FirstAsync(x => x.Id == id);

            string newName = "Push-up EDITED";

            var viewModel = new ExerciseDetailViewModel()
            {
                Id = id,
                Name = newName,
                Description = exerciseToEdit.Description,
                VideoUrl = exerciseToEdit.VideoUrl,
                MuscleGroup = exerciseToEdit.MuscleGroup,
            };

            bool succeed = await _exerciseService.EditExerciseAsync(viewModel);

            int expectedExerciseInDb = 3;
            int actualExerciseInDb = await _applicationDbContext.Exercises.CountAsync();
            var editedExercise = await _applicationDbContext.Exercises.FindAsync(id);

            Assert.That(succeed);
            Assert.That(actualExerciseInDb, Is.EqualTo(expectedExerciseInDb));
            Assert.That(editedExercise, Is.Not.Null);
            Assert.That(editedExercise.Name, Is.EqualTo(newName));
            Assert.That(editedExercise.Description, Is.EqualTo(exerciseToEdit.Description));
            Assert.That(editedExercise.VideoUrl, Is.EqualTo(exerciseToEdit.VideoUrl));
            Assert.That(editedExercise.MuscleGroup, Is.EqualTo(exerciseToEdit.MuscleGroup));
        }

        [Test]
        public void EditExerciseAsync_ShouldThrowNullReferenceException()
        {
            var viewModel = new ExerciseDetailViewModel() { Id = 5, };

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _exerciseService.EditExerciseAsync(viewModel));
        }

        [Test]
        public async Task DeleteExerciseAsync_ShouldMakePropertyIsDeleteTrue()
        {
            int id = 2;

            bool succeed = await _exerciseService.DeleteExerciseAsync(id);

            var deletedExercise = await _applicationDbContext.Exercises.FindAsync(id);

            Assert.That(succeed);
            Assert.That(deletedExercise, Is.Not.Null);
            Assert.That(deletedExercise.IsDelete, Is.EqualTo(true));
        }

        [Test]
        public void DeleteExerciseAsync_ShouldThrowNullReferenceException()
        {
            int id = 8;

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _exerciseService.DeleteExerciseAsync(id));
        }

        [TestCase(1, 3)]
        [TestCase(2, 2)]
        public async Task AllExerciseAsync_ShouldReturnAllForEachPage(int currPage, int expectedResultCount)
        {
            var exerciseListTwoMore = new List<Exercise>()
            {
                new Exercise
                {
                    Id = 4,
                    Name = "Deadlift",
                    Description = "The deadlift is a weight training exercise in which a loaded barbell or bar is lifted off the        ground     to the level of the hips, then lowered back to the ground.",
                    VideoUrl = "https://www.example.com/deadlift_video",
                    MuscleGroup = MuscleGroup.Legs,
                },

                new Exercise
                {
                    Id = 5,
                    Name = "Bench Press",
                    Description = "The bench press is an upper body strength training exercise that consists of pressing a  weight       upwards from a supine position.",
                    VideoUrl = "https://www.example.com/bench_press_video",
                    MuscleGroup = MuscleGroup.Chest,
                }
            };
            await _applicationDbContext.AddRangeAsync(exerciseListTwoMore);
            await _applicationDbContext.SaveChangesAsync();

            var query = new AllExercisesQueryModel()
            {
                CurrentPage = currPage
            };

            IEnumerable<ExerciseAllViewModel> result =
                await _exerciseService.AllExerciseAsync(query);

            int allExerciseInDb = await _applicationDbContext.Exercises.Where(x => x.IsDelete == false).CountAsync();

            IEnumerable<ExerciseAllViewModel> sortedCollection =
                 await _applicationDbContext.Exercises
                .Where(x => x.IsDelete == false)
                .Select(x => new ExerciseAllViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    MuscleGroup = x.MuscleGroup,
                    UsedByProgramsCount = x.FitnessProgramsExercises.Count()
                })
                .OrderByDescending(x => x.Id)
                .Skip((query.CurrentPage - 1) * query.ExercisePerPage)
                .Take(query.ExercisePerPage)
                .ToListAsync();

            Assert.That(result.Count(), Is.EqualTo(expectedResultCount));
            Assert.That(query.TotalExerciseCount, Is.EqualTo(allExerciseInDb));
            Assert.That(result.First().Id, Is.EqualTo(sortedCollection.First().Id));
            Assert.That(result.Last().Id, Is.EqualTo(sortedCollection.Last().Id));
        }

        [TestCase(Sorting.Interactions)]
        [TestCase(Sorting.Аlphabetical)]
        public async Task AllExerciseAsync_ShouldSort(Sorting sorting)
        {
            var query = new AllExercisesQueryModel()
            {
                Sorting = sorting
            };

            IEnumerable<ExerciseAllViewModel> result =
                await _exerciseService.AllExerciseAsync(query);


            IEnumerable<ExerciseAllViewModel> sortedCollection =
                await _applicationDbContext.Exercises
               .Where(x => x.IsDelete == false)
               .Select(x => new ExerciseAllViewModel()
               {
                   Id = x.Id,
                   Name = x.Name,
                   MuscleGroup = x.MuscleGroup,
                   UsedByProgramsCount = x.FitnessProgramsExercises.Count()
               })
               .ToListAsync();

            if (sorting == Sorting.Interactions)
            {
                sortedCollection = sortedCollection.OrderByDescending(x => x.UsedByProgramsCount).ToList();
            }
            else
            {
                sortedCollection = sortedCollection.OrderBy(x => x.Name).ToList();
            }

            Assert.That(result.First().Id, Is.EqualTo(sortedCollection.First().Id));
            Assert.That(result.Last().Id, Is.EqualTo(sortedCollection.Last().Id));
        }

        [TestCase("Pu")]
        [TestCase("Sq")]
        public async Task AllExerciseAsync_ShouldSearchTermReturn(string searchTerm)
        {
            var query = new AllExercisesQueryModel()
            {
                SearchTerm = searchTerm
            };

            IEnumerable<ExerciseAllViewModel> result =
                await _exerciseService.AllExerciseAsync(query);


            IEnumerable<ExerciseAllViewModel> sortedCollection =
                await _applicationDbContext.Exercises
               .Where(x => x.IsDelete == false && x.Name.ToLower().Contains(searchTerm.ToLower()))
               .Select(x => new ExerciseAllViewModel()
               {
                   Id = x.Id,
                   Name = x.Name,
                   MuscleGroup = x.MuscleGroup,
                   UsedByProgramsCount = x.FitnessProgramsExercises.Count()
               })
               .OrderByDescending(x => x.Id)
               .ToListAsync();

            Assert.That(result.First().Id, Is.EqualTo(sortedCollection.First().Id));
            Assert.That(result.Last().Id, Is.EqualTo(sortedCollection.Last().Id));
        }

        [TestCase(MuscleGroup.Chest)]
        [TestCase(MuscleGroup.Legs)]
        public async Task AllExerciseAsync_ShouldCategotyReturn(MuscleGroup category)
        {
            var query = new AllExercisesQueryModel()
            {
                Category = category
            };

            IEnumerable<ExerciseAllViewModel> result =
                await _exerciseService.AllExerciseAsync(query);


            IEnumerable<ExerciseAllViewModel> sortedCollection =
                await _applicationDbContext.Exercises
               .Where(x => x.IsDelete == false && x.MuscleGroup == category)
               .Select(x => new ExerciseAllViewModel()
               {
                   Id = x.Id,
                   Name = x.Name,
                   MuscleGroup = x.MuscleGroup,
                   UsedByProgramsCount = x.FitnessProgramsExercises.Count()
               })
               .OrderByDescending(x => x.Id)
               .ToListAsync();

            Assert.That(result.First().Id, Is.EqualTo(sortedCollection.First().Id));
            Assert.That(result.Last().Id, Is.EqualTo(sortedCollection.Last().Id));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(1)]
        public async Task FindExerciseByIdAsync_ShouldReturnConcreteExerciseById(int id)
        {
            var returnedExercise = await _exerciseService.FindExerciseByIdAsync(id);

            var expected = await _applicationDbContext
                .Exercises
                .Where(x => x.IsDelete == false)
                .Select(x => new ExerciseDetailViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    VideoUrl = x.VideoUrl,
                    MuscleGroup = x.MuscleGroup,
                    UsedByProgramsCount = x.FitnessProgramsExercises.Count()
                })
                .FirstAsync(x => x.Id == id);

            Assert.That(returnedExercise, Is.Not.Null);
            Assert.That(returnedExercise.Id, Is.EqualTo(expected.Id));
            Assert.That(returnedExercise.Name, Is.EqualTo(expected.Name));
            Assert.That(returnedExercise.Description, Is.EqualTo(expected.Description));
            Assert.That(returnedExercise.VideoUrl, Is.EqualTo(expected.VideoUrl));
            Assert.That(returnedExercise.MuscleGroup, Is.EqualTo(expected.MuscleGroup));
            Assert.That(returnedExercise.UsedByProgramsCount, Is.EqualTo(expected.UsedByProgramsCount));
        }

        [Test]
        public void FindExerciseByIdAsync_ShouldThrowNullReferenceException()
        {
            int id = 8;

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _exerciseService.FindExerciseByIdAsync(id));
        }

        [Test]
        public async Task GetAllExerciseForProgramAsync_ShouldReturnAllExerciseForProgramNotAlreadyInIt()
        {
            int programId = 3;

            var returned = await _exerciseService.GetAllExerciseForProgramAsync(programId);

            var expected = await _applicationDbContext
                .Exercises
                .Where(x => x.IsDelete == false && x.Id != 2 && x.Id != 3)
                .Select(x => new ExerciseForProgramViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            Assert.That(returned.Count(), Is.EqualTo(expected.Count));
            Assert.That(returned.First().Id, Is.EqualTo(expected.First().Id));
            Assert.That(returned.First().Name, Is.EqualTo(expected.First().Name));
        }

        [Test]
        public async Task AddExerciseToProgramAsync_ShouldAddExerciseInSelectedProgram()
        {
            int fitnessProgramId = 3;
            int exerciseId = 1;

            var viewModel = new FitnessProgramExercisesInfoViewModel()
            {
                FitnessProgramId = fitnessProgramId,
                ExerciseId = exerciseId,
                Reps = 10,
                Sets = 12,
                Weight = 35,
                Order = 2
            };

            bool succeed = await _exerciseService.AddExerciseToProgramAsync(viewModel);

            int expectedCount = 3;
            int exercisesInFitnessProgramId3 = await _applicationDbContext
                .FitnessProgramsExercises
                .Where(x => x.FitnessProgramId == fitnessProgramId)
                .CountAsync();

            var returned = await _applicationDbContext
                .FitnessProgramsExercises
                .FirstOrDefaultAsync(x => x.FitnessProgramId == fitnessProgramId && x.ExerciseId == exerciseId);

            Assert.That(succeed, Is.True);
            Assert.That(exercisesInFitnessProgramId3, Is.EqualTo(expectedCount));
            Assert.That(returned, Is.Not.Null);
        }

        [Test]
        public async Task EditExerciseFromProgramAsync_ShouldEditExerciseParametersInSelectedProgram()
        {
            int fitnessProgramId = 3;
            int exerciseId = 2;

            var viewModel = new FitnessProgramExercisesInfoViewModel()
            {
                FitnessProgramId = fitnessProgramId,
                ExerciseId = exerciseId,
                Reps = 3,
                Sets = 15,
                Weight = 55,
                Order = 6
            };

            bool succeed = await _exerciseService.EditExerciseFromProgramAsync(viewModel);

            var afterFitnessProgramExercise = await _applicationDbContext
               .FitnessProgramsExercises
               .FirstAsync(x => x.FitnessProgramId == fitnessProgramId && x.ExerciseId == exerciseId);

            Assert.That(succeed, Is.True);
            Assert.That(afterFitnessProgramExercise.Reps, Is.EqualTo(viewModel.Reps));
            Assert.That(afterFitnessProgramExercise.Sets, Is.EqualTo(viewModel.Sets));
            Assert.That(afterFitnessProgramExercise.Weight, Is.EqualTo(viewModel.Weight));
            Assert.That(afterFitnessProgramExercise.Order, Is.EqualTo(viewModel.Order));
        }

        [Test]
        public void EditExerciseFromProgramAsync_ShouldThrowNullReferenceException()
        {
            var viewModel = new FitnessProgramExercisesInfoViewModel()
            {
                FitnessProgramId = 5,
                ExerciseId = 2,
            };

            Assert.ThrowsAsync<NullReferenceException>(async ()
                => await _exerciseService.EditExerciseFromProgramAsync(viewModel));
        }

        [Test]
        public async Task RemoveExerciseFromProgramAsync_ShouldRemoveExerciseFromFitnessProgram()
        {
            int fitnessProgramId = 3;
            int exerciseId = 2;

            bool succeed = await _exerciseService.RemoveExerciseFromProgramAsync(exerciseId, fitnessProgramId);

            var afterFitnessProgramExercise = await _applicationDbContext
               .FitnessProgramsExercises
               .FirstOrDefaultAsync(x => x.FitnessProgramId == fitnessProgramId && x.ExerciseId == exerciseId);

            Assert.That(succeed, Is.True);
            Assert.That(afterFitnessProgramExercise, Is.Null);
        }

        [Test]
        public void RemoveExerciseFromProgramAsync_ShouldThrowNullReferenceException()
        {
            int fitnessProgramId = 5;
            int exerciseId = 4;

            Assert.ThrowsAsync<NullReferenceException>(async ()
                 => await _exerciseService.RemoveExerciseFromProgramAsync(exerciseId, fitnessProgramId));
        }

        [Test]
        public async Task GetExerciseFromProgramToEditAsync_ShouldReturnSelectedExerciseFromSelectedFitenssProgram()
        {
            int fitnessProgramId = 3;
            int exerciseId = 2;

            var returnedExercise = await _exerciseService.GetExerciseFromProgramToEditAsync(exerciseId, fitnessProgramId);

            var expectedExercise = await _applicationDbContext
                .FitnessProgramsExercises
                .Select(x => new FitnessProgramExercisesInfoViewModel()
                {
                    FitnessProgramId = x.FitnessProgramId,
                    ExerciseId = x.ExerciseId,
                    ExerciseName = x.Exercise.Name,
                    Reps = x.Reps,
                    Sets = x.Sets,
                    Weight = x.Weight,
                    Order = x.Order
                })
                .FirstAsync(x => x.FitnessProgramId == fitnessProgramId && x.ExerciseId == exerciseId);

            Assert.That(returnedExercise.ExerciseId, Is.EqualTo(expectedExercise.ExerciseId));
            Assert.That(returnedExercise.FitnessProgramId, Is.EqualTo(expectedExercise.FitnessProgramId));
            Assert.That(returnedExercise.ExerciseName, Is.EqualTo(expectedExercise.ExerciseName));
            Assert.That(returnedExercise.Reps, Is.EqualTo(expectedExercise.Reps));
            Assert.That(returnedExercise.Sets, Is.EqualTo(expectedExercise.Sets));
            Assert.That(returnedExercise.Weight, Is.EqualTo(expectedExercise.Weight));
            Assert.That(returnedExercise.Order, Is.EqualTo(expectedExercise.Order));
        }

        [Test]
        public void GetExerciseFromProgramToEditAsync_ShouldThrowNullReferenceException()
        {
            int fitnessProgramId = 5;
            int exerciseId = 4;

            Assert.ThrowsAsync<NullReferenceException>(async ()
                 => await _exerciseService.GetExerciseFromProgramToEditAsync(exerciseId, fitnessProgramId));
        }

        [TestCase(false, 2)]
        [TestCase(true, 1)]
        public async Task AllExerciseForAdminAsync_ShouldReturnAllActiveOrDeletedExercise(bool deleted, int expectedCount)
        {
            await _exerciseService.DeleteExerciseAsync(2);

            var returnedExercise = await _exerciseService.AllExerciseForAdminAsync(deleted);
            int returnedCount = returnedExercise.Count();

            Assert.That(returnedCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task RestoreExerciseAsync_ShouldMakeIsDeleteFalse()
        {
            int exerciseId = 2;

            await _exerciseService.DeleteExerciseAsync(exerciseId);

            var beforeAllActiveExercise = await _applicationDbContext
                .Exercises
                .Where(x => x.IsDelete == false)
                .ToListAsync();

            int beforeAllActiveExerciseCount = beforeAllActiveExercise.Count();
            int expectedBefor = 2;

            bool succeed = await _exerciseService.RestoreExerciseAsync(exerciseId);

            var afterAllActiveExercise = await _applicationDbContext
                .Exercises
                .Where(x => x.IsDelete == false)
                .ToListAsync();

            int afterAllActiveExerciseCount = afterAllActiveExercise.Count();
            int expectedAfter = 3;

            var returnedExercise = await _applicationDbContext
                .Exercises
                .Where(x => x.IsDelete == false)
                .FirstOrDefaultAsync(x => x.Id == exerciseId);

            Assert.That(beforeAllActiveExerciseCount, Is.EqualTo(expectedBefor));
            Assert.That(succeed, Is.True);
            Assert.That(afterAllActiveExerciseCount, Is.EqualTo(expectedAfter));
            Assert.That(returnedExercise, Is.Not.Null);
            Assert.That(returnedExercise.IsDelete, Is.False);
        }

        [Test]
        public void RestoreExerciseAsync_ShouldThrowNullReferenceException()
        {
            int exerciseId = 2;

            Assert.ThrowsAsync<NullReferenceException>(async ()
                 => await _exerciseService.RestoreExerciseAsync(exerciseId));
        }
    }
}