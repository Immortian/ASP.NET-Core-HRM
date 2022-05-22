using HRM.Application.Interfaces;
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

        [HttpPut]
        public async Task<IActionResult> UpdateAchievementsList(List<PersonalAchievement> achievements)
        {
            foreach(var achiev in achievements)
            {
                await context.Achievement.UpdateAsync(achiev);
            }
            await context.Save();
            return Ok();
        }
    }
}
