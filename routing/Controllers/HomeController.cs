using Microsoft.AspNetCore.Mvc;

namespace routing.Controllers
{
    [Route("h")]
    [Route("[controller]")]
    public class HomeController: Controller
    {
        [Route("")]
        [Route("[action]")]
        public string Index()
        {
            return "Hello from HomeController.Index";
        }
    }
}