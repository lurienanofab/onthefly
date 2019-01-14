using System.Web.Http;

namespace OnTheFly.Controllers
{
    public class ApiDefaultController : ApiController
    {
        [Route("api")]
        public string Get()
        {
            return "otf-api";
        }
    }
}
