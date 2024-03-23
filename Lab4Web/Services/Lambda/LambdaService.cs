using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        // a. Expresii lambda
        // i. Fara parametri
        [HttpGet("LambdaWithoutParameters")]
        public IActionResult LambdaWithoutParameters()
        {
            // Definirea expresiei lambda fara parametri
            Func<int> lambdaExp = () => 42;
            int result = _lambdaService.TestLambdaWithoutParameters(lambdaExp);
            return Ok(result);
        }

        // ii. Un parametru
        [HttpGet("LambdaWithOneParameter")]
        public IActionResult LambdaWithOneParameter(int num)
        {
            // Definirea expresiei lambda cu un parametru
            Func<int, int> lambdaExp = (x) => x * x;
            int result = _lambdaService.TestLambdaWithOneParameter(lambdaExp, num);
            return Ok(result);
        }

        // iii. Doi parametri
        [HttpGet("LambdaWithTwoParameters")]
        public IActionResult LambdaWithTwoParameters(int a, int b)
        {
            // Definirea expresiei lambda cu doi parametri
            Func<int, int, int> lambdaExp = (x, y) => x + y;
            int result = _lambdaService.TestLambdaWithTwoParameters(lambdaExp, a, b);
            return Ok(result);
        }

        // iv. Parametri neutilizati in expresie
        [HttpGet("LambdaUnusedParameters")]
        public IActionResult LambdaUnusedParameters(int a, int b)
        {
            // Definirea expresiei lambda cu parametri neutilizati
            Func<int, int, int> lambdaExp = (_, _) => a + b;
            int result = _lambdaService.TestLambdaUnusedParameters(lambdaExp);
            return Ok(result);
        }

        // v. Parametri cu valori default
        [HttpGet("LambdaWithDefaultParameters")]
        public IActionResult LambdaWithDefaultParameters(int a = 1, int b = 2)
        {
            // Definirea expresiei lambda cu parametri cu valori default
            Func<int, int, int> lambdaExp = (x, y) => x * y;
            int result = _lambdaService.TestLambdaWithDefaultParameters(lambdaExp, a, b);
            return Ok(result);
        }

        // vi. Tuple ca parametru
        [HttpGet("LambdaWithTupleParameter")]
        public IActionResult LambdaWithTupleParameter(int value)
        {
            // Definirea expresiei lambda cu un tuplu ca parametru
            Func<int, Tuple<int, int, int>> lambdaExp = (num) => new Tuple<int, int, int>(num % 10, (num /= 10) % 10, (num /= 10) % 10);
            var result = _lambdaService.TestLambdaWithTupleParameter(lambdaExp, value);
            return Ok(result);
        }

        // b. Expresii lambda in context asincron
        [HttpGet("LambdaAsync")]
        public async Task<IActionResult> LambdaAsync(string value)
        {
            // Definirea expresiei lambda in context asincron
            Func<string, Task<string>> lambdaExp = async (v) =>
            {
                await Task.Delay(1000); // simulare asteptare
                return v.ToUpper();
            };

            var result = await _lambdaService.TestLambdaAsync(lambdaExp, value);
            return Ok(result);
        }
    }

    public interface ILambdaService
    {
        int TestLambdaWithoutParameters(Func<int> lambdaExp);

        int TestLambdaWithOneParameter(Func<int, int> lambdaExp, int num);

        int TestLambdaWithTwoParameters(Func<int, int, int> lambdaExp, int a, int b);

        int TestLambdaUnusedParameters(Func<int, int, int> lambdaExp);

        int TestLambdaWithDefaultParameters(Func<int, int, int> lambdaExp, int a = 1, int b = 2);

        Tuple<int, int, int> TestLambdaWithTupleParameter(Func<int, Tuple<int, int, int>> lambdaExp, int value);

        Task<string> TestLambdaAsync(Func<string, Task<string>> lambdaExp, string value);
    }

    public class LambdaService : ILambdaService
    {
        public int TestLambdaWithoutParameters(Func<int> lambdaExp)
        {
            return lambdaExp();
        }

        public int TestLambdaWithOneParameter(Func<int, int> lambdaExp, int num)
        {
            return lambdaExp(num);
        }

        public int TestLambdaWithTwoParameters(Func<int, int, int> lambdaExp, int a, int b)
        {
            return lambdaExp(a, b);
        }

        public int TestLambdaUnusedParameters(Func<int, int, int> lambdaExp)
        {
            return lambdaExp(0, 0);
        }

        public int TestLambdaWithDefaultParameters(Func<int, int, int> lambdaExp, int a = 1, int b = 2)
        {
            return lambdaExp(a, b);
        }

        public Tuple<int, int, int> TestLambdaWithTupleParameter(Func<int, Tuple<int, int, int>> lambdaExp, int value)
        {
            return lambdaExp(value);
        }

        public async Task<string> TestLambdaAsync(Func<string, Task<string>> lambdaExp, string value)
        {
            return await lambdaExp(value);
        }
    }
}
