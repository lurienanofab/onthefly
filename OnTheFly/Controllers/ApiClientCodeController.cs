using OnTheFly.Models;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;

namespace OnTheFly.Controllers
{
    public class ApiClientCodeController : ApiController
    {
        [HttpGet]
        public void GetLatestCode([FromUri] ApiModel model, string filename)
        {
            string otfFilesPath = ConfigurationManager.AppSettings["otf:Files"];

            /* 
			 * Binarywrite is choosen as other methods adding extra spaces/lines when transfering data
			 * from Windows machine to linux(extra \r\n) and no gaps. (IIS is converting the data to string-unicode).
			 */

            HttpContext.Current.Response.ContentType = "text/plain";
            byte[] b = File.ReadAllBytes(otfFilesPath + filename);
            HttpContext.Current.Response.BinaryWrite(b);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        [HttpGet]
        public string _GetLatestCode(string filename)
        {
            string otfFilesPath = ConfigurationManager.AppSettings["otf:Files"];
            string content = File.ReadAllText(otfFilesPath + filename);
            return content;
        }
    }
}
