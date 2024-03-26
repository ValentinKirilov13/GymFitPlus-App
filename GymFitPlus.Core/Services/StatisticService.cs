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
        private readonly IFitnessProgramService _fitnessProgramService;

        public StatisticService(IRepository repository, IFitnessProgramService fitnessProgramService)
        {
            _repository = repository;
            _fitnessProgramService = fitnessProgramService;
        }

        public async Task<IEnumerable<UserStatsViewModel>> GetUserConcreteStatsAsync(string stats, Guid userId)
        {
            IQueryable<UserSatistics> statistics = _repository
                 .AllReadOnly<UserSatistics>()
                 .Where(x => x.UserId == userId)
                 .OrderBy(x => x.DateOfМeasurements);

            IQueryable<UserStatsViewModel> selectedResult = stats switch
            {
                "Weight" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  Weight = x.Weight,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "Height" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  Height = x.Height,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "ChestCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  ChestCircumference = x.ChestCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "BackCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  BackCircumference = x.BackCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "WaistCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  WaistCircumference = x.WaistCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "RightArmCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  RightArmCircumference = x.RightArmCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "LeftArmCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  LeftArmCircumference = x.LeftArmCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "GluteusCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  GluteusCircumference = x.GluteusCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "LeftLegCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  LeftLegCircumference = x.LeftLegCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "RightLegCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  RightLegCircumference = x.RightLegCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "LeftCalfCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  LeftCalfCircumference = x.LeftCalfCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                "RightCalfCircumference" =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  RightCalfCircumference = x.RightCalfCircumference,
                  DateOfМeasurements = x.DateOfМeasurements
              }),
                _ =>
              statistics.
              Select(x => new UserStatsViewModel()
              {
                  Weight = x.Weight,
                  DateOfМeasurements = x.DateOfМeasurements
              })
            };


            return await selectedResult.ToListAsync();
        }

        public async Task<UserStatsViewModel> GetUserLastAllStatsAsync(Guid userId)
        {
            return await _repository
                .AllReadOnly<UserSatistics>()
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
                .All<UserSatistics>()
                .Where(x => x.UserId == viewModel.UserId)
                .FirstOrDefaultAsync(x => x.DateOfМeasurements == DateTime.Today);

            if (statsFromDb == null)
            {
                var statsModel = new UserSatistics()
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
