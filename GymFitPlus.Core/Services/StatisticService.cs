using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.StatisticViewModels;
using GymFitPlus.Infrastructure.Data.Common;
using GymFitPlus.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GymFitPlus.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository _repository;

        public StatisticService(IRepository repository)
        {
            _repository = repository;           
        }

        public async Task<IEnumerable<UserStatsViewModel>> GetUserConcreteStatsAsync(string stats, Guid userId)
        {
            IQueryable<UserStatistics> statistics = _repository
                 .AllReadOnly<UserStatistics>()
                 .Where(x => x.UserId == userId)
                 .OrderBy(x => x.DateOfМeasurements);

            IQueryable<UserStatsViewModel> selectedResult = stats switch
            {
                "Weight" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  Weight = Math.Round(x.Weight, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "Height" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  Height = Math.Round(x.Height, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "ChestCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  ChestCircumference = Math.Round(x.ChestCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "BackCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  BackCircumference = Math.Round(x.BackCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "WaistCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  WaistCircumference = Math.Round(x.WaistCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "RightArmCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  RightArmCircumference = Math.Round(x.RightArmCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "LeftArmCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  LeftArmCircumference = Math.Round(x.LeftArmCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "GluteusCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  GluteusCircumference = Math.Round(x.GluteusCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "LeftLegCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  LeftLegCircumference = Math.Round(x.LeftLegCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "RightLegCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  RightLegCircumference = Math.Round(x.RightLegCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "LeftCalfCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  LeftCalfCircumference = Math.Round(x.LeftCalfCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "RightCalfCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  RightCalfCircumference = Math.Round(x.RightCalfCircumference, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                _ =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  Weight = Math.Round(x.Weight, 1),
                  DateOfМeasurements = x.DateOfМeasurements
              })
            };


            return await selectedResult.ToListAsync();
        }

        public async Task<UserStatsViewModel> GetUserLastAllStatsAsync(Guid userId)
        {
            return await _repository
                .AllReadOnly<UserStatistics>()
                .Where(x => x.UserId == userId)
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
                .FirstOrDefaultAsync() ?? throw new NullReferenceException();
        }

        public async Task UpdateUserStatsAsync(UserStatsViewModel viewModel)
        {
            var statsFromDb = await _repository
                .All<UserStatistics>()
                .Where(x => x.UserId == viewModel.UserId)
                .FirstOrDefaultAsync(x => x.DateOfМeasurements == DateTime.Today);

            if (statsFromDb == null)
            {
                var statsModel = new UserStatistics()
                {
                    UserId = viewModel.UserId,
                    DateOfМeasurements = viewModel.DateOfМeasurements,
                    Weight = viewModel.Weight,
                    Height = viewModel.Height,
                    ChestCircumference = viewModel.ChestCircumference,
                    BackCircumference = viewModel.BackCircumference,
                    WaistCircumference = viewModel.WaistCircumference,
                    RightArmCircumference = viewModel.RightArmCircumference,
                    LeftArmCircumference = viewModel.LeftArmCircumference,
                    GluteusCircumference = viewModel.GluteusCircumference,
                    LeftLegCircumference = viewModel.LeftLegCircumference,
                    RightLegCircumference = viewModel.RightLegCircumference,
                    LeftCalfCircumference = viewModel.LeftCalfCircumference,
                    RightCalfCircumference = viewModel.RightCalfCircumference
                };

                await _repository.AddAsync(statsModel);
            }
            else
            {
                statsFromDb.UserId = viewModel.UserId;
                statsFromDb.DateOfМeasurements = viewModel.DateOfМeasurements;
                statsFromDb.Weight = viewModel.Weight;
                statsFromDb.Height = viewModel.Height;
                statsFromDb.ChestCircumference = viewModel.ChestCircumference;
                statsFromDb.BackCircumference = viewModel.BackCircumference;
                statsFromDb.WaistCircumference = viewModel.WaistCircumference;
                statsFromDb.RightArmCircumference = viewModel.RightArmCircumference;
                statsFromDb.LeftArmCircumference = viewModel.LeftArmCircumference;
                statsFromDb.GluteusCircumference = viewModel.GluteusCircumference;
                statsFromDb.LeftLegCircumference = viewModel.LeftLegCircumference;
                statsFromDb.RightLegCircumference = viewModel.RightLegCircumference;
                statsFromDb.LeftCalfCircumference = viewModel.LeftCalfCircumference;
                statsFromDb.RightCalfCircumference = viewModel.RightCalfCircumference;
            }

            await _repository.SaveChangesAsync();
        }
    }
}
