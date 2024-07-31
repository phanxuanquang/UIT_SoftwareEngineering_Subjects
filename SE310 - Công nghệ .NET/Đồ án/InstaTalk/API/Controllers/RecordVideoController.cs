using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RecordVideoController : BaseApiController
    {
        private readonly IConfiguration _configuration;
        public RecordVideoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> SaveRecoredFile()
        {
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files;
            if (files.Any())
            {
                var file = files["video-blob"];
                string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedRecordFiles");
                string UniqueFileName = User.Identity.Name + "_" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Minute + ".webm";
                string UploadPath = Path.Combine(UploadFolder, UniqueFileName);

                using (var temp = new FileStream(UploadPath, FileMode.Create))
                {
                    await file.CopyToAsync(temp);// close stream sau khi copy xong do khoi lenh using
                }

                return Ok($"{_configuration.GetValue<string>("DomainHosting")}/wwwroot/UploadedRecordFiles/{UniqueFileName}");
            }
            else
            {
                return BadRequest("No file created");
            }
        }
    }
}
