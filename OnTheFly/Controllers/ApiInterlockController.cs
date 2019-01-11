using LNF.CommonTools;
using OnTheFly.Models;
using System;
using System.Web.Http;

namespace OnTheFly.Controllers
{
    public class ApiInterlockController : ApiController
    {
        [HttpGet, Route("api/interlock")]
        public InterlockResponse GetState(int resourceId)
        {
            bool state = WagoInterlock.GetPointState(resourceId);
            return new InterlockResponse() { State = state, Message = state ? "ON" : "OFF" };
        }

        [HttpGet, Route("api/interlock/on")]
        public InterlockResponse On(int resourceId)
        {
            WagoInterlock.ToggleInterlock(resourceId, true, 0);
            return new InterlockResponse() { State = true, Message = "TURN_ON_OK" };
        }

        [HttpGet, Route("api/interlock/off")]
        public InterlockResponse Off(int resourceId)
        {
            WagoInterlock.ToggleInterlock(resourceId, false, 0);
            return new InterlockResponse() { State = true, Message = "TURN_OFF_OK" };
        }

        [HttpGet, Route("api/interlock/bypass")]
        public InterlockResponse Bypass(int resourceId, int duration)
        {
            uint d = (uint)Math.Max(0, duration);
            WagoInterlock.ToggleInterlock(resourceId, true, d);
            return new InterlockResponse() { State = true, Message = "BYPASS_OK" };
        }
    }
}