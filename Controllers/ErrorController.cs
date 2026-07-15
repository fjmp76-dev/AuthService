using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Throw()
        {
            var ex = new Exception("This is a test exception for the global exception handler.");
            throw ex;
        }
    }
}
