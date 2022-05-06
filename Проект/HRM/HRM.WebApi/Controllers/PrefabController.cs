using HRM.Application.Interfaces;
using HRM.Persistence;
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
    public class PrefabController : Controller
    {
        private readonly IUnitOfWork context;
        public PrefabController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SetPrefab(IFormFile file)
        {
            
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            var prefab = new Domain.File(); //await context.File.GetByIdAsync(1);
            prefab.Name = "prefab.docx";
            prefab.Data = fileBytes;
            await context.File.CreateAsync(prefab);
            //await context.Save();
            return Ok();
        }

        [HttpGet]
        public async Task<FileStreamResult> GetPrefab()
        {
            string path = @"wwwroot/files/prefab.docx";
            //byte[] file = context.File.GetByIdAsync(2).Result.Data;
            //using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    await stream.WriteAsync(file, 0, file.Length);
            //}

            byte[] prefabData = null;
            if (context.File.GetByIdAsync(1).Result is not null)
                prefabData = context.File.GetByIdAsync(1).Result.Data;

            using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await stream.WriteAsync(prefabData, 0, prefabData.Length);
            }

            var fileStream = System.IO.File.OpenRead(path);
            return File(fileStream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }
    }
}
