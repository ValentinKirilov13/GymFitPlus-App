using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.Services;
using GymFitPlus.Core.ViewModels.StatisticViewModels;
using GymFitPlus.Infrastructure.Data;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.UnitTests
{
    [TestFixture]
    public class StatisticServiceTests
    {
        private IStatisticService _statisticService;
        private IRepository _repository;
        private ApplicationDbContext _applicationDbContext;
        private List<UserStatistics> _userStatistics;
        private Guid _firstUserGuid;
        private Guid _secondUserGuid;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "StatisticsDB")
               .Options;

            _applicationDbContext = new ApplicationDbContext(contextOptions);
            _repository = new Repository(_applicationDbContext);

            _firstUserGuid = Guid.Parse("c6291132-e05b-491a-84ee-1049d1f036dc");
            _secondUserGuid = Guid.Parse("9e9a6477-ca79-4b99-86aa-bf8feb829123");

            _userStatistics = new List<UserStatistics>()
            {
                new UserStatistics
                {
                    Id = 1,
                    UserId = _firstUserGuid,
                    DateOfМeasurements = DateTime.Today,
                    Weight = 70.5,
                    Height = 1.75,
                    ChestCircumference = 95,
                    BackCircumference = 40,
                    RightArmCircumference = 30,
                    LeftArmCircumference = 29,
                    WaistCircumference = 80,
                    GluteusCircumference = 100,
                    RightLegCircumference = 55,
                    LeftLegCircumference = 54,
                    RightCalfCircumference = 35,
                    LeftCalfCircumference = 34
                },
                new UserStatistics
                {
                    Id = 2,
                    UserId = _firstUserGuid,
                    DateOfМeasurements = DateTime.Now.Date.AddDays(-7),
                    Weight = 68.2,
                    Height = 1.80,
                    ChestCircumference = 92,
                    BackCircumference = 39,
                    RightArmCircumference = 29,
                    LeftArmCircumference = 28,
                    WaistCircumference = 78,
                    GluteusCircumference = 98,
                    RightLegCircumference = 53,
                    LeftLegCircumference = 52,
                    RightCalfCircumference = 34,
                    LeftCalfCircumference = 33
                },
                new UserStatistics
                {
                    Id = 3,
                    UserId = _firstUserGuid,
                    DateOfМeasurements = DateTime.Now.Date.AddDays(-7),
                    Weight = 68.2,
                    Height = 1.80,
                    ChestCircumference = 92,
                    BackCircumference = 39,
                    RightArmCircumference = 29,
                    LeftArmCircumference = 28,
                    WaistCircumference = 6,
                    GluteusCircumference = 88,
                    RightLegCircumference = 53,
                    LeftLegCircumference = 56,
                    RightCalfCircumference = 34,
                    LeftCalfCircumference = 33
                },
                new UserStatistics
                {
                    Id = 4,
                    UserId = _secondUserGuid,
                    DateOfМeasurements = DateTime.Now.Date.AddDays(-7),
                    Weight = 68.2,
                    Height = 1.84,
                    ChestCircumference = 92,
                    BackCircumference = 39,
                    RightArmCircumference = 29,
                    LeftArmCircumference = 24,
                    WaistCircumference = 6,
                    GluteusCircumference = 88,
                    RightLegCircumference = 63,
                    LeftLegCircumference = 6,
                    RightCalfCircumference = 8,
                    LeftCalfCircumference = 33
                },
                new UserStatistics
                {
                    Id = 5,
                    UserId = _secondUserGuid,
                    DateOfМeasurements = DateTime.Now.Date.AddDays(-7),
                    Weight = 68.2,
                    Height = 1.84,
                    ChestCircumference = 92,
                    BackCircumference = 39,
                    RightArmCircumference = 29,
                    LeftArmCircumference = 2,
                    WaistCircumference = 6,
                    GluteusCircumference = 88,
                    RightLegCircumference = 23,
                    LeftLegCircumference = 2,
                    RightCalfCircumference = 8,
                    LeftCalfCircumference = 33
                },
            };

            _applicationDbContext.AddRange(_userStatistics);
            _applicationDbContext.SaveChanges();

            _statisticService = new StatisticService(_repository);
        }

        [TearDown]
        public void TearDown()
        {
            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Dispose();
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsWeightStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("Weight", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    Weight = Math.Round(x.Weight, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().Weight, Is.EqualTo(actualStats.First().Weight));
            Assert.That(returnStats.Last().Weight, Is.EqualTo(actualStats.Last().Weight));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsHeightStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("Height", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    Height = Math.Round(x.Height, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().Height, Is.EqualTo(actualStats.First().Height));
            Assert.That(returnStats.Last().Height, Is.EqualTo(actualStats.Last().Height));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsChestCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("ChestCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    ChestCircumference = Math.Round(x.ChestCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().ChestCircumference, Is.EqualTo(actualStats.First().ChestCircumference));
            Assert.That(returnStats.Last().ChestCircumference, Is.EqualTo(actualStats.Last().ChestCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsBackCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("BackCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    BackCircumference = Math.Round(x.BackCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().BackCircumference, Is.EqualTo(actualStats.First().BackCircumference));
            Assert.That(returnStats.Last().BackCircumference, Is.EqualTo(actualStats.Last().BackCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsWaistCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("WaistCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    WaistCircumference = Math.Round(x.WaistCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().WaistCircumference, Is.EqualTo(actualStats.First().WaistCircumference));
            Assert.That(returnStats.Last().WaistCircumference, Is.EqualTo(actualStats.Last().WaistCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsRightArmCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("RightArmCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    RightArmCircumference = Math.Round(x.RightArmCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().RightArmCircumference, Is.EqualTo(actualStats.First().RightArmCircumference));
            Assert.That(returnStats.Last().RightArmCircumference, Is.EqualTo(actualStats.Last().RightArmCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsLeftArmCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("LeftArmCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    LeftArmCircumference = Math.Round(x.LeftArmCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().LeftArmCircumference, Is.EqualTo(actualStats.First().LeftArmCircumference));
            Assert.That(returnStats.Last().LeftArmCircumference, Is.EqualTo(actualStats.Last().LeftArmCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsGluteusCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("GluteusCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    GluteusCircumference = Math.Round(x.GluteusCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().GluteusCircumference, Is.EqualTo(actualStats.First().GluteusCircumference));
            Assert.That(returnStats.Last().GluteusCircumference, Is.EqualTo(actualStats.Last().GluteusCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsLeftLegCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("LeftLegCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    LeftLegCircumference = Math.Round(x.LeftLegCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().LeftLegCircumference, Is.EqualTo(actualStats.First().LeftLegCircumference));
            Assert.That(returnStats.Last().LeftLegCircumference, Is.EqualTo(actualStats.Last().LeftLegCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsRightLegCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("RightLegCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    RightLegCircumference = Math.Round(x.RightLegCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().RightLegCircumference, Is.EqualTo(actualStats.First().RightLegCircumference));
            Assert.That(returnStats.Last().RightLegCircumference, Is.EqualTo(actualStats.Last().RightLegCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsLeftCalfCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("LeftCalfCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    LeftCalfCircumference = Math.Round(x.LeftCalfCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().LeftCalfCircumference, Is.EqualTo(actualStats.First().LeftCalfCircumference));
            Assert.That(returnStats.Last().LeftCalfCircumference, Is.EqualTo(actualStats.Last().LeftCalfCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsRightCalfCircumferenceStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync("RightCalfCircumference", Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    RightCalfCircumference = Math.Round(x.RightCalfCircumference, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().RightCalfCircumference, Is.EqualTo(actualStats.First().RightCalfCircumference));
            Assert.That(returnStats.Last().RightCalfCircumference, Is.EqualTo(actualStats.Last().RightCalfCircumference));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc", 3)]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123", 2)]
        public async Task GetUserConcreteStatsAsync_ShouldReturnsDefaultStats(string userId, int expectedCountStats)
        {
            var returnStats = await _statisticService
                .GetUserConcreteStatsAsync(string.Empty, Guid.Parse(userId));

            var actualStats = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderBy(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    Weight = Math.Round(x.Weight, 1),
                    DateOfМeasurements = x.DateOfМeasurements
                })
                .ToListAsync();

            Assert.That(returnStats.Count(), Is.EqualTo(expectedCountStats));
            Assert.That(returnStats.First().Weight, Is.EqualTo(actualStats.First().Weight));
            Assert.That(returnStats.Last().Weight, Is.EqualTo(actualStats.Last().Weight));
        }

        [TestCase("c6291132-e05b-491a-84ee-1049d1f036dc")]
        [TestCase("9e9a6477-ca79-4b99-86aa-bf8feb829123")]
        public async Task GetUserLastAllStatsAsync_ShouldReturnCurrentUserLastStats(string userId)
        {
            var returned = await _statisticService.GetUserLastAllStatsAsync(Guid.Parse(userId));

            var expected = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == Guid.Parse(userId))
                .OrderByDescending(x => x.DateOfМeasurements)
                .Select(x => new UserStatsViewModel()
                {
                    Weight = x.Weight,
                    Height = x.Height,
                    ChestCircumference = x.ChestCircumference,
                    BackCircumference = x.BackCircumference,
                    WaistCircumference = x.WaistCircumference,
                    RightArmCircumference = x.RightArmCircumference,
                    LeftArmCircumference = x.LeftArmCircumference,
                    GluteusCircumference = x.GluteusCircumference,
                    LeftLegCircumference = x.LeftLegCircumference,
                    RightLegCircumference = x.RightLegCircumference,
                    LeftCalfCircumference = x.LeftCalfCircumference,
                    RightCalfCircumference = x.RightCalfCircumference
                })
                .FirstAsync();

            Assert.That(returned, Is.Not.Null);
            Assert.That(returned.Weight, Is.EqualTo(expected.Weight));
            Assert.That(returned.Height, Is.EqualTo(expected.Height));
            Assert.That(returned.ChestCircumference, Is.EqualTo(expected.ChestCircumference));
            Assert.That(returned.BackCircumference, Is.EqualTo(expected.BackCircumference));
            Assert.That(returned.WaistCircumference, Is.EqualTo(expected.WaistCircumference));
            Assert.That(returned.RightArmCircumference, Is.EqualTo(expected.RightArmCircumference));
            Assert.That(returned.LeftArmCircumference, Is.EqualTo(expected.LeftArmCircumference));
            Assert.That(returned.GluteusCircumference, Is.EqualTo(expected.GluteusCircumference));
            Assert.That(returned.LeftLegCircumference, Is.EqualTo(expected.LeftLegCircumference));
            Assert.That(returned.RightLegCircumference, Is.EqualTo(expected.RightLegCircumference));
            Assert.That(returned.LeftCalfCircumference, Is.EqualTo(expected.LeftCalfCircumference));
            Assert.That(returned.RightCalfCircumference, Is.EqualTo(expected.RightCalfCircumference));
        }

        [TestCase("c8295132-e05b-491a-84ee-1049d1f036dc")]
        [TestCase("c8295572-e05b-491a-84ee-1049d1f036dc")]
        public void GetUserLastAllStatsAsync_ShouldThrowNullReferenceException(string userId)
        {
            Assert.ThrowsAsync<NullReferenceException>(async ()
               => await _statisticService.GetUserLastAllStatsAsync(Guid.Parse(userId)));
        }

        [Test]
        public async Task UpdateUserStatsAsync_ShouldUpdateStatsIfTheyExistForCurrentDay()
        {
            var viewModel = new UserStatsViewModel()
            {
                UserId = _firstUserGuid,
                DateOfМeasurements = DateTime.Today,
                Weight = 15,
                Height = 78,
                ChestCircumference = 54,
                BackCircumference = 21,
                WaistCircumference = 54,
                RightArmCircumference = 16,
                LeftArmCircumference = 90,
                GluteusCircumference = 43,
                LeftLegCircumference = 21,
                RightLegCircumference = 34,
                LeftCalfCircumference = 45,
                RightCalfCircumference = 24
            };

            var countDataInDbBefor = await _applicationDbContext
               .UserStatistics
               .Where(x => x.UserId == _firstUserGuid)
               .ToListAsync();

            bool succeed = await _statisticService.UpdateUserStatsAsync(viewModel);

            var dataAfterInDb = await _applicationDbContext
               .UserStatistics
               .Where(x => x.UserId == _firstUserGuid)
               .Select(x => new UserStatsViewModel()
               {
                   UserId = x.UserId,
                   DateOfМeasurements = x.DateOfМeasurements,
                   Weight = x.Weight,
                   Height = x.Height,
                   ChestCircumference = x.ChestCircumference,
                   BackCircumference = x.BackCircumference,
                   WaistCircumference = x.WaistCircumference,
                   RightArmCircumference = x.RightArmCircumference,
                   LeftArmCircumference = x.LeftArmCircumference,
                   GluteusCircumference = x.GluteusCircumference,
                   LeftLegCircumference = x.LeftLegCircumference,
                   RightLegCircumference = x.RightLegCircumference,
                   LeftCalfCircumference = x.LeftCalfCircumference,
                   RightCalfCircumference = x.RightCalfCircumference
               })
               .FirstAsync(x => x.DateOfМeasurements == DateTime.Today);

            var countDataInDbAfter = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == _firstUserGuid)
                .ToListAsync();

            Assert.That(succeed, Is.True);
            Assert.That(countDataInDbAfter.Count(), Is.EqualTo(countDataInDbBefor.Count()));
            Assert.That(dataAfterInDb.UserId, Is.EqualTo(viewModel.UserId));
            Assert.That(dataAfterInDb.DateOfМeasurements, Is.EqualTo(viewModel.DateOfМeasurements));
            Assert.That(dataAfterInDb.Weight, Is.EqualTo(viewModel.Weight));
            Assert.That(dataAfterInDb.Height, Is.EqualTo(viewModel.Height));
            Assert.That(dataAfterInDb.ChestCircumference, Is.EqualTo(viewModel.ChestCircumference));
            Assert.That(dataAfterInDb.BackCircumference, Is.EqualTo(viewModel.BackCircumference));
            Assert.That(dataAfterInDb.WaistCircumference, Is.EqualTo(viewModel.WaistCircumference));
            Assert.That(dataAfterInDb.RightArmCircumference, Is.EqualTo(viewModel.RightArmCircumference));
            Assert.That(dataAfterInDb.LeftArmCircumference, Is.EqualTo(viewModel.LeftArmCircumference));
            Assert.That(dataAfterInDb.GluteusCircumference, Is.EqualTo(viewModel.GluteusCircumference));
            Assert.That(dataAfterInDb.LeftLegCircumference, Is.EqualTo(viewModel.LeftLegCircumference));
            Assert.That(dataAfterInDb.RightLegCircumference, Is.EqualTo(viewModel.RightLegCircumference));
            Assert.That(dataAfterInDb.LeftCalfCircumference, Is.EqualTo(viewModel.LeftCalfCircumference));
            Assert.That(dataAfterInDb.RightCalfCircumference, Is.EqualTo(viewModel.RightCalfCircumference));
        }

        [Test]
        public async Task UpdateUserStatsAsync_ShouldAddNewStatsIfTheyDontExistForCurrentDay()
        {
            var viewModel = new UserStatsViewModel()
            {
                UserId = _secondUserGuid,
                DateOfМeasurements = DateTime.Today,
                Weight = 15,
                Height = 78,
                ChestCircumference = 54,
                BackCircumference = 21,
                WaistCircumference = 54,
                RightArmCircumference = 16,
                LeftArmCircumference = 90,
                GluteusCircumference = 43,
                LeftLegCircumference = 21,
                RightLegCircumference = 34,
                LeftCalfCircumference = 45,
                RightCalfCircumference = 24
            };

            bool succeed = await _statisticService.UpdateUserStatsAsync(viewModel);

            var returned = await _applicationDbContext
               .UserStatistics
               .Where(x => x.UserId == _secondUserGuid)
               .FirstOrDefaultAsync(x => x.DateOfМeasurements == DateTime.Today);

            int expectedCountInDb = 3;
            var countDataInDbAfter = await _applicationDbContext
                .UserStatistics
                .Where(x => x.UserId == _secondUserGuid)
                .ToListAsync();

            Assert.That(succeed, Is.True);
            Assert.That(returned, Is.Not.Null);
            Assert.That(countDataInDbAfter.Count(), Is.EqualTo(expectedCountInDb));
        }
    }
}
