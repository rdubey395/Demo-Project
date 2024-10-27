using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(PolicyName = "DemoOrigin")]
    [Authorize(AuthenticationSchemes = "DemoJwt", Roles = "Super Admin")]
    public class DemoController : Controller
    {
        private readonly IMyLogger _mylogger;
        public DemoController(IMyLogger myLogger)
        {
            // Loosely coupled
            _mylogger = myLogger;
            //tightly coupled
            //_mylogger = new LogToServer();
           // _mylogger = new LogToFile();
        }
        [HttpGet]
        public ActionResult Index()
        {
            _mylogger.Log("Index method started");
            return Ok("This is Demo");
        }
    }
}
