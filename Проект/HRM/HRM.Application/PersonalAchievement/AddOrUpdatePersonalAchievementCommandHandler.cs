using HRM.Application.Interfaces;
using HRM.Domain;

namespace HRM.Application.AchievementsHandling
{
    public class AddOrUpdatePersonalAchievementCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public AddOrUpdatePersonalAchievementCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddOrUpdate(AddOrUpdatePersonalAchievementCommand request)
        {
            var achiev = new PersonalAchievement()
            {
                AchievementId = request.AchievementId,
                Description = request.Description,
                EmployeeId = request.EmployeeId,
                PeriodId = request.PeriodId,
                Reward = request.Reward
            };
            await unitOfWork.Achievement.UpdateAsync(achiev);
            await unitOfWork.Save();
        }
    }
}
