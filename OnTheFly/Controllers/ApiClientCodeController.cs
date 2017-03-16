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
        [HttpGet, Route("api/clientcode/test")]
        public string Test()
        {
            return "test";
        }

        [HttpGet, Route("api/clientcode/zgetlatestcode")]
        public void zGetLatestCode([FromUri] ApiModel model, string filename)
        {
            string otfFilesPath = ConfigurationManager.AppSettings["otf:Files"];

            /* 
			 * Binarywrite is choosen as other methods adding extra spaces/lines when transfering data
			 * from Windows machine to linux (extra \r\n) and no gaps. (IIS is converting the data to string-unicode).
			 */

            HttpContext.Current.Response.ContentType = "text/plain";
            byte[] b = File.ReadAllBytes(Path.Combine(otfFilesPath,filename));
            HttpContext.Current.Response.BinaryWrite(b);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        [HttpGet, Route("api/clientcode/getlatestcode")]
        public HttpResponseMessage GetLatestCode(string filename)
        {
            string otfFilesPath = ConfigurationManager.AppSettings["otf:Files"];

            /* 
			 * Binarywrite is choosen as other methods adding extra spaces/lines when transfering data
			 * from Windows machine to linux (extra \r\n) and no gaps. (IIS is converting the data to string-unicode).
			 */

            string fileContents = File.ReadAllText(Path.Combine(otfFilesPath, filename));

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(fileContents, Encoding.UTF8, "text/plain");
            return result;

            //HttpContext.Current.Response.ContentType = "text/plain";
            //byte[] b = File.ReadAllBytes(Path.Combine(otfFilesPath, filename));
            //HttpContext.Current.Response.BinaryWrite(b);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
        }
    }
}
