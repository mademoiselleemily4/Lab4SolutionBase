using Microsoft.AspNetCore.Mvc;
using System;

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

        // a. Folositi un Delegate pentru a insera apelarea unei metode intr-o alta metoda
        [HttpGet("InsertMethodCall")]
        public IActionResult InsertMethodCall(string name)
        {
            // Definirea unui Delegate pentru a apela metoda Hello din DelegateService
            Func<string, string, string> callback = (firstname, lastname) => _delegateService.Hello(firstname, lastname);

            // Apelarea metodei Introduction din DelegateService, in care se apeleaza metoda Hello utilizand Delegate-ul definit anterior
            string result = _delegateService.Introduction(name, callback);
            return Ok(result);
        }

        // b. Folositi una sau mai multe metode Delegate si demonstrati executia uneia dintre ele in functie de o conditie impusa
        [HttpGet("ConditionalDelegate")]
        public IActionResult ConditionalDelegate(string name, bool useUpperCase)
        {
            // Definirea unui Delegate care va apela metoda Hello din DelegateService in functie de conditia useUpperCase
            Func<string, string, string> callback = useUpperCase ? (Func<string, string, string>)((firstname, lastname) => _delegateService.Hello(firstname.ToUpper(), lastname.ToUpper())) : _delegateService.Hello;

            // Apelarea metodei Introduction din DelegateService, care utilizeaza Delegate-ul definit anterior
            string result = _delegateService.Introduction(name, callback);
            return Ok(result);
        }

        // c. Folositi Delegate pentru a apela mai multe metode consecutive la finalul unei metode create de voi
        [HttpGet("MultipleMethodCalls")]
        public IActionResult MultipleMethodCalls(string name)
        {
            // Definirea unui Delegate pentru a apela metoda Hello din DelegateService
            Func<string, string, string> callback = (firstname, lastname) => _delegateService.Hello(firstname, lastname);

            // Apelarea metodei Introduction din DelegateService, care utilizeaza Delegate-ul definit anterior
            string result = _delegateService.Introduction(name, callback);

            // Apelarea unei alte metode (Goodbye) la finalul metodei Introduction
            result += Environment.NewLine + _delegateService.Goodbye("John");

            return Ok(result);
        }
    }

    public interface IDelegateService
    {
        string Introduction(string value, Func<string, string, string> callback);

        string Hello(string firstname, string lastname);

        string Goodbye(string name);
    }

    public class DelegateService : IDelegateService
    {
        public string Introduction(string value, Func<string, string, string> callback)
        {
            // Convertirea valorii la majuscule
            var upperName = value.ToUpper();
            // Apelarea Delegate-ului pentru a obtine rezultatul final
            return callback(upperName, "Test");
        }

        public string Hello(string firstname, string lastname)
        {
            // Construirea unui mesaj de salut
            return $"Hello, {firstname} {lastname}";
        }

        public string Goodbye(string name)
        {
            // Construirea unui mesaj de despartire
            return $"Goodbye, {name}";
        }
    }
}
