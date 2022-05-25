using HRM.Application.Interfaces;
using HRM.Application.AchievementsHandling;
using HRM.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AchievementsController : Controller
    {
        private readonly IUnitOfWork context;
        public AchievementsController(IUnitOfWork context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<List<PersonalAchievement>> GetAchievementsList()
        {
            return await context.Achievement.GetAllAsync();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAchievementsList(AddOrUpdatePersonalAchievementCommand request)
        {
            var handler = new AddOrUpdatePersonalAchievementCommandHandler(context);
            await handler.AddOrUpdate(request);
            return Ok();
        }
    }
}
