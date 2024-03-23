using Lab4Web.Services.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Lab4Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestLinqController : ControllerBase
    {
        private readonly ILinqService _linqService;

        public TestLinqController(ILinqService linqService)
        {
            _linqService = linqService;
        }

        [HttpGet("test-1")]
        public int Get(int age)
        {
            return _linqService.Test1(age);
        }
    }
}
