using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.Services;
using GymFitPlus.Core.ViewModels.WorkoutViewModels;
using GymFitPlus.Infrastructure.Data;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.UnitTests
{
    [TestFixture]
    public class WorkoutServiceTests
    {
        private IWorkoutService _workoutService;
        private IRepository _repository;
        private ApplicationDbContext _applicationDbContext;
        private List<Workout> _workouts;
        private List<FitnessProgram> _fitnessPrograms;
        private Guid _firstUserId;
        private Guid _secondUserId;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "WorkoutDB")
               .Options;

            _applicationDbContext = new ApplicationDbContext(contextOptions);
            _repository = new Repository(_applicationDbContext);

            _firstUserId = Guid.Parse("c6291132-e05b-491a-84ee-1049d1f036dc");
            _secondUserId = Guid.Parse("9e9a6477-ca79-4b99-86aa-bf8feb829123");
            _workouts = new List<Workout>()
            {
                new Workout { Id = 1, UserId = _firstUserId, FitnessProgramId = 1, Date = DateTime.Now.Date, Duration = 60, Note = "Great workout!" },
                new Workout { Id = 2, UserId = _firstUserId, FitnessProgramId = 2, Date = DateTime.Now.Date.AddDays(-1), Duration = 45, Note = "Feeling tired today." },
                new Workout { Id = 3, UserId = _firstUserId, FitnessProgramId = 1, Date = DateTime.Now.Date.AddDays(-2), Duration = 55, Note = "Pushed myself hard." },
                new Workout { Id = 4, UserId = _secondUserId, FitnessProgramId = 3, Date = DateTime.Now.Date.AddDays(-3), Duration = 70, Note = "New ersonal best!" },
                new Workout { Id = 5, UserId = _secondUserId, FitnessProgramId = 2, Date = DateTime.Now.Date.AddDays(-4), Duration = 50, Note = "Enjoyed the cardio." }
            };
            _fitnessPrograms = new List<FitnessProgram>()
            {
               new FitnessProgram { Id = 1, Name = "Push"},
               new FitnessProgram { Id = 2, Name = "Push"},
               new FitnessProgram { Id = 3, Name = "Legs"},
            };

            _applicationDbContext.AddRange(_fitnessPrograms);
            _applicationDbContext.AddRange(_workouts);

            _applicationDbContext.SaveChanges();

            _workoutService = new WorkoutService(_repository);
        }

        [TearDown]
        public void TearDown()
        {
            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Dispose();
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        [TestCase("1e417e65-87c6-4b32-bba5-0a0ad1c1eea6", 0)]
        public async Task GetAllWorkoutsAsync_ShouldReturnAllWorkoutsForCurrentUser(string userId, int expectedWorkoutsCount)
        {
            var returnedWorkouts = await _workoutService.GetAllWorkoutsAsync(Guid.Parse(userId));

            int returnedCount = returnedWorkouts.Count();

            Assert.That(returnedCount, Is.EqualTo(expectedWorkoutsCount));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 2)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 5)]
        public async Task GetByIdWorkoutAsync_ShouldReturnSelectedWorkout(string userId, int workoutId)
        {
            var returned = await _workoutService.GetByIdWorkoutAsync(workoutId, Guid.Parse(userId));

            var expected = await _applicationDbContext
                .Workouts
                .Select(x => new WorkoutDetailViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FitnessProgramName = x.FitnessProgram.Name,
                    Date = x.Date,
                    Duration = x.Duration,
                    FitnessProgramId = x.FitnessProgramId,
                    Note = x.Note
                })
                .FirstAsync(x => x.Id == workoutId && x.UserId == Guid.Parse(userId));

            Assert.That(returned, Is.Not.Null);
            Assert.That(returned.Id, Is.EqualTo(expected.Id));
            Assert.That(returned.FitnessProgramId, Is.EqualTo(expected.FitnessProgramId));
            Assert.That(returned.UserId, Is.EqualTo(expected.UserId));
            Assert.That(returned.FitnessProgramName, Is.EqualTo(expected.FitnessProgramName));
            Assert.That(returned.Date, Is.EqualTo(expected.Date));
            Assert.That(returned.Duration, Is.EqualTo(expected.Duration));
            Assert.That(returned.Note, Is.EqualTo(expected.Note));
        }

        [TestCase("c8295132-e05b-491a-84ee-1049d1f036dc", 8)]
        [TestCase("c8295572-e05b-491a-84ee-1049d1f036dc", 9)]
        public void GetByIdWorkoutAsync_ShouldThrowNullReferenceException(string userId, int workoutId)
        {
            Assert.ThrowsAsync<NullReferenceException>(async ()
               => await _workoutService.GetByIdWorkoutAsync(workoutId, Guid.Parse(userId)));
        }

        [Test]
        public async Task CreateWorkoutAsync_ShouldCreateAndAddWorkoutIntoDatabase()
        {
            var viewModel = new WorkoutDetailViewModel()
            {
                UserId = Guid.Parse("c8295132-e05b-491a-84ee-1049d1f036dc"),
                FitnessProgramId = 2,
                Date = DateTime.Now,
                Duration = 120,
                Note = "Great workout",
            };

            bool succeed = await _workoutService.CreateWorkoutAsync(viewModel);

            var workoutCountInDb = await _applicationDbContext
                .Workouts
                .ToListAsync();

            int expectedWorkoutsInDb = 6;
            int actualWorkoutsInDb = workoutCountInDb.Count();

            var returnedWorkout = await _applicationDbContext
                .Workouts
                .FirstOrDefaultAsync(x => x.Id == 6 
                    && x.UserId == Guid.Parse("c8295132-e05b-491a-84ee-1049d1f036dc")
                    && x.FitnessProgramId == 2);

            Assert.That(succeed, Is.True);
            Assert.That(actualWorkoutsInDb, Is.EqualTo(expectedWorkoutsInDb));
            Assert.That(returnedWorkout, Is.Not.Null);
        }

        [Test]
        public async Task DeleteWorkoutAsync_ShouldRemoveExerciseFromDb()
        {
            var viewModel = new WorkoutDetailViewModel() { Id = 2 };
            var userId = Guid.Parse("c6291132-e05b-491a-84ee-1049d1f036dc");

            bool succeed = await _workoutService.DeleteWorkoutAsync(viewModel,userId);

            var workoutsInDb = await _applicationDbContext
                .Workouts
                .ToListAsync();

            int expectedWorkoutsInDb = 4;
            int actualWorkoutsInDb = workoutsInDb.Count();

            var returned = await _applicationDbContext
                .Workouts
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id && x.UserId == userId);

            Assert.That(succeed, Is.True);
            Assert.That(actualWorkoutsInDb, Is.EqualTo(expectedWorkoutsInDb));
            Assert.That(returned, Is.Null);
        }

        [Test]
        public void DeleteWorkoutAsync_ShouldThrowNullReferenceException()
        {
            var viewModel = new WorkoutDetailViewModel() { Id = 9 };
            var userId = Guid.Parse("c6295432-e05b-491a-84ee-1049d1f036dc");

            Assert.ThrowsAsync<NullReferenceException>(async ()
               => await _workoutService.DeleteWorkoutAsync(viewModel, userId));
        }
    }
}
