using Application.FileMeta;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FileManagerController : BaseApiController
    {
        [HttpGet("files")]
        public async Task<IActionResult> GetFiles()
        {
            var currentUser = await GetCurrentUserAsync();
            return HandleResult(await Mediator.Send(new List.Query { AppUserId = currentUser.Id }));
        }

    }
}