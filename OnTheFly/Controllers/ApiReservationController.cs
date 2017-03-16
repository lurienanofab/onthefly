using LabScheduler.AppCode;
using LNF.CommonTools;
using LNF.Repository.Scheduler;
using OnTheFly.Models;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnTheFly.Controllers
{
    public class ApiReservationController : ApiController
    {
        [HttpGet, Route("api/reservation/cardswipe")]
        public async Task<ApiModel> CardSwipe([FromUri] ApiModel model, string cardswipedata, int buttonindex)
        {
            OnTheFlyResource otfr = ReservationOnTheFlyUtil.GetOnTheFlyResource(cardswipedata, buttonindex);

            if (null == otfr)
            {
                model.ServerResponse = "OnTheFlyResource not found for " + cardswipedata + " , buttonindex: " + buttonindex.ToString();
            }
            else
            {
                OnTheFlyImpl oimp = new OnTheFlyImpl(otfr, cardswipedata, Request.GetOwinContext().Request.RemoteIpAddress);
                await oimp.Swipe();

                string strFS = (oimp.IsProcessFailed()) ? "failed" : "started";
                model.ServerResponse = otfr.GetResourceTypeAsString() + "_reservation_" + strFS;
                model.ResourceType = otfr.ResourceType;
                bool returnLog = Utility.StringToBoolean(ConfigurationManager.AppSettings["otf:ReturnFullLog"]);

                if (returnLog)
                {
                    model.ServerResponse += oimp.GetReturnMessage();
                }
            }

            return model;
        }
    }

}
