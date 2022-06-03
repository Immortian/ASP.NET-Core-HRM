using HRM.Application.Interfaces;
using HRM.Application.WorkLoadDistribution.CreateDistribution;
using HRM.Application.WorkLoadDistribution.GenerateAddendum;
using HRM.Application.WorkLoadDistribution.UpdateDistribution;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace HRM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DistributionController : Controller
    {
        private IUnitOfWork context { get; }

        public DistributionController(IUnitOfWork _context)
        {
            context = _context;
        }

        [HttpPost]
        public async Task<IActionResult> Distribute(CreateDistributionCommand request)
        {
            var handler = new CreateDistributionCommandHandler(context);
            await handler.Distribute(request);
            return Ok(request);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDistribution(UpdateDistributionCommand request)
        {
            var handler = new UpdateDistributionCommandHandler(context);
            await handler.UpdateDistribution(request);
            return Ok(request);
        }

        [HttpHead("Addendum")]
        public async Task<ActionResult> GenerateAddendum()
        {
            var handler = new GenerateAddendumCommandHandler(context);
            foreach (var employeeLoad in context.EmployeeWorkLoad.GetByPeriodId(1))
            {
                await handler.GenerateAddendum(
                new GenerateAddendumCommand
                {
                    PeriodId = 1,
                    EmployeeId = employeeLoad.EmployeeId,
                    WorkLoad = employeeLoad.WorkLoadHours
                });
            }
            await context.File.ClearStorage(@"wwwroot/files/");
            return Ok();
        }
        [HttpGet("Addendum/{id}")]
        public async Task<FileStreamResult> GetAddendum(int id)
        {
            var addendum = context.File.GetOne(id, context.Period.GetPeriodByDate(DateTime.Now).PeriodId);
            var adData = addendum.Data;
            var path = @"wwwroot/files/" + addendum.Name;
            using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await stream.WriteAsync(adData, 0, adData.Length);
            }

            var fileStream = System.IO.File.OpenRead(path);
            return File(fileStream, "application /vnd.openxmlformats-officedocument.wordprocessingml.document");
        }

        [HttpGet("Addendum/Period/{periodid}")]
        public async Task<FileStreamResult> DownloadFolder(int periodid)
        {
            string path;
            var current = await context.Period.GetByIdAsync(periodid);
            foreach (var addendum in context.File.GetAllByPeriodId(periodid))
            {
                var adData = addendum.Data;
                path = @"wwwroot/files/" + addendum.Name;
                using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    await stream.WriteAsync(adData);
                }
            }
            path = @"wwwroot/files/";
            FileStreamResult fileStreamResult;
            var tempPath = Path.Combine(Path.GetTempPath(), "temp.zip");

            path = path.Remove(path.Length - 1);
            ZipFile.CreateFromDirectory(path, tempPath, CompressionLevel.Fastest, false);
            FileStream fileStreamInput = new FileStream(tempPath, FileMode.Open, FileAccess.Read, FileShare.Delete);
            fileStreamResult = new FileStreamResult(fileStreamInput, "APPLICATION/octet-stream");
            fileStreamResult.FileDownloadName =  context.Period.GetDateFromPeriod(current).ToString("Y") + ".zip";
           
            if (System.IO.File.Exists(tempPath))
            {
                System.IO.File.Delete(tempPath);
                context.File.ClearStorage(path + "/");
            }
            return fileStreamResult;
        }
    }
}
