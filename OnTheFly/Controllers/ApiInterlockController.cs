using LNF.CommonTools;
using OnTheFly.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnTheFly.Controllers
{
    public class ApiInterlockController : ApiController
    {
        [HttpGet, Route("api/interlock")]
        public async Task<InterlockResponse> GetState(int resourceId)
        {
            bool state = await WagoInterlock.GetPointState(resourceId);
            return new InterlockResponse() { State = state, Message = state ? "ON" : "OFF" };
        }

        [HttpGet, Route("api/interlock/on")]
        public async Task<InterlockResponse> On(int resourceId)
        {
            await WagoInterlock.ToggleInterlock(resourceId, true, 0);
            return new InterlockResponse() { State = true, Message = "TURN_ON_OK" };
        }

        [HttpGet, Route("api/interlock/off")]
        public async Task<InterlockResponse> Off(int resourceId)
        {
            await WagoInterlock.ToggleInterlock(resourceId, false, 0);
            return new InterlockResponse() { State = true, Message = "TURN_OFF_OK" };
        }

        [HttpGet, Route("api/interlock/bypass")]
        public async Task<InterlockResponse> Bypass(int resourceId, int duration)
        {
            uint d = (uint)Math.Max(0, duration);
            await WagoInterlock.ToggleInterlock(resourceId, true, d);
            return new InterlockResponse() { State = true, Message = "BYPASS_OK" };
        }
    }
}