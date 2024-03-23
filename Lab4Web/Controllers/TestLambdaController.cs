using Lab4Web.Services.Lambda;
using Microsoft.AspNetCore.Mvc;

namespace Lab4Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestLambdaController : ControllerBase
    {
        private readonly ILambdaService _lambdaService;

        public TestLambdaController(ILambdaService lambdaService)
        {
            _lambdaService = lambdaService;
        }

        [HttpGet("test-1")]
        public string Get(int value)
        {
            var tupleValue = _lambdaService.Test1(value);
            return $"{tupleValue.Item1} / {tupleValue.Item2} / {tupleValue.Item3}";
        }

        [HttpGet("test-2")]
        public string Test2(string value)
        {
            return _lambdaService.Test2(value) ? "Number" : "Not number";
        }

        [HttpGet("test-3")]
        public string Test3(string value)
        {
            var result = _lambdaService.Test3Async(value).Result;
            return result;
        }
    }
}
