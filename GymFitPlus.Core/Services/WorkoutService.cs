using GymFitPlus.Core.Contracts;
using GymFitPlus.Infrastructure.Data.Common;

namespace GymFitPlus.Core.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IRepository _repository;

        public WorkoutService(IRepository repository)
        {
            _repository = repository;
        }



    }
}
