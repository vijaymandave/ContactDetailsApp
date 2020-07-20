using System.Web.Http;

namespace WebApiApp.Controllers
{
    public class TestController : ApiController
    {
        public string Get()
        {
            return "Hello!...Contact Web api up and running.";
        }
    }
}
