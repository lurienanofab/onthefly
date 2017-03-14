using LabScheduler.AppCode;
using LNF.CommonTools;
using LNF.Repository.Scheduler;
using OnTheFly.Models;
using System.Configuration;
using System.Web.Http;

namespace OnTheFly.Controllers
{
    public class ApiReservationController : ApiController
    {
        [HttpGet]
        public ApiModel CardSwipe([FromUri] ApiModel model, string cardswipedata, int buttonindex)
        {
            //if (!model.IsValidIP(System.Web.HttpContext.Current))
            //{
            //	model.ServerResponse = "InvalidIP";
            //	return model;
            //}

            OnTheFlyResource otfr = ReservationOnTheFlyUtil.GetOnTheFlyResource(cardswipedata, buttonindex);

            if (null == otfr)
            {
                model.ServerResponse = "OnTheFlyResource not found for " + cardswipedata + " , buttonindex: " + buttonindex.ToString();
            }
            else
            {
                OnTheFlyImpl oimp = new OnTheFlyImpl(otfr, cardswipedata, System.Web.HttpContext.Current.Request.UserHostAddress);
                oimp.Swipe();

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
