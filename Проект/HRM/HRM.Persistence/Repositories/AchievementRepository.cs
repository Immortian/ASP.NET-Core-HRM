using HRM.Application.BuisnessLogic;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;

namespace HRM.Persistence.Repositories
{
    public class AchievementRepository : RepositoryBase<PersonalAchievement>, IAchievementRepository
    {
        public AchievementRepository(HRMDBContext context) : base(context)
        {

        }
    }
}
