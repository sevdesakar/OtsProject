using Microsoft.AspNetCore.Mvc;
using Ots.Api.Impl.PerformanceTest;

namespace Ots.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsertPerformanceController : ControllerBase
    {
        private readonly InsertPerformanceTester _performanceTester;

        public InsertPerformanceController(InsertPerformanceTester performanceTester)
        {
            _performanceTester = performanceTester;
        }

        [HttpPost("run-tests")]
        public async Task<IActionResult> RunInsertTests()
        {
            await _performanceTester.RunInsertTestsAsync();

            return Ok("Performans testleri tamamlandı. Log dosyasına yazıldı.");
        }
    }
}
