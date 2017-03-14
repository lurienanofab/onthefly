using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net;
using System.Net.Http;
using System.Configuration;
using LabScheduler.AppCode;

namespace OnTheFly
{
    public class OnTheFlyAuthorizeAttribute : AuthorizeAttribute
    {
        private string message;
        private string[] whitelist;

        public OnTheFlyAuthorizeAttribute(params string[] whitelist)
        {
            //no args: use appSettings or anyone can access
            this.message = string.Empty;
            this.whitelist = whitelist;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, message);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (ReservationOnTheFlyUtil.IsValidIP(GetIPAddress()))
                return true;

            return ErrorResult("Unauthorized");
        }

        private bool ErrorResult(string errmsg)
        {
            message = errmsg;
            return false;
        }

        private bool SuccessResult()
        {
            message = string.Empty;
            return true;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        private string GetIPAddress()
        {
            try
            {
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }

                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}