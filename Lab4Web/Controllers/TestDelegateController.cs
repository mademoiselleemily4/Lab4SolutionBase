using Lab4Web.Services.Delegate;
using Microsoft.AspNetCore.Mvc;

namespace Lab4Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestDelegateController : ControllerBase
    {
        private readonly IDelegateService _delegateService;

        public TestDelegateController(IDelegateService delegateService)
        {
            _delegateService = delegateService;
        }

        [HttpGet("test-1")]
        public string Test1(string name)
        {
            var callback = _delegateService.Hello;

            return _delegateService.Introduction(name, callback);
        }

        [HttpGet("test-2")]
        public string Test2(string name, bool welcome)
        {
            var callback1 = _delegateService.Hello;
            var callback2 = (string firstname, string lastname) => $"Bye, {firstname} {lastname}";

            var callback = welcome ? callback1 : callback2;

            return _delegateService.Introduction(name, callback);
        }
    }
}
