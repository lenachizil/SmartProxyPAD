using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncNode.Services;

namespace SyncNode.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly SyncWorkJobService _WorkJobService;

        public SyncController(SyncWorkJobService workJobService)
        {
            _WorkJobService = workJobService;
        }

        [HttpPost]
        public IActionResult Sync(SyncEntity entity)
        {
            _WorkJobService.AddItem(entity);
            return Ok();
        }
    }
}
 