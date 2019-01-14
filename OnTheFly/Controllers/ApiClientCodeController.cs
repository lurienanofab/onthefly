using OnTheFly.Models;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace OnTheFly.Controllers
{
    public class ApiClientCodeController : ApiController
    {
        [HttpGet, Route("api/clientcode/getlatestcode")]
        public HttpResponseMessage GetLatestCode(string filename)
        {
            string otfFilesPath = ConfigurationManager.AppSettings["otf:Files"];
            string path = Path.Combine(otfFilesPath, filename);
            var result = GetFile(path);
            return result;
        }

        [HttpGet, Route("api/clientcode/getlatestcode/{version}")]
        public HttpResponseMessage GetLatestCode(string version, string filename)
        {
            string otfFilesPath = ConfigurationManager.AppSettings["otf:Files"];
            string path = Path.Combine(otfFilesPath, version, filename);
            var result = GetFile(path);
            return result;
        }

        private HttpResponseMessage GetFile(string path)
        {
            var fileContents = File.ReadAllText(path);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(fileContents, Encoding.UTF8, "text/plain");
            return result;
        }
    }
}
