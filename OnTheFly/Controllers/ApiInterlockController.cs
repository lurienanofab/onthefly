using LNF.CommonTools;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnTheFly.Controllers
{
    public class ApiInterlockController : ApiController
    {
        public async Task<InterlockResponse> GetState(int resourceId)
        {
            bool state = await WagoInterlock.GetPointState(resourceId);
            return new InterlockResponse() { State = state, Message = state ? "ON" : "OFF" };
        }

        [HttpGet]
        public async Task<InterlockResponse> On(int resourceId)
        {
            await WagoInterlock.ToggleInterlock(resourceId, true, 0);
            return new InterlockResponse() { State = true, Message = "TURN_ON_OK" };
        }

        [HttpGet]
        public async Task<InterlockResponse> Off(int resourceId)
        {
            await WagoInterlock.ToggleInterlock(resourceId, false, 0);
            return new InterlockResponse() { State = true, Message = "TURN_OFF_OK" };
        }

        [HttpGet]
        public async Task<InterlockResponse> Bypass(int resourceId, int duration)
        {
            uint d = (uint)Math.Max(0, duration);
            await WagoInterlock.ToggleInterlock(resourceId, true, d);
            return new InterlockResponse() { State = true, Message = "BYPASS_OK" };
        }
    }

    public class InterlockResponse
    {
        public bool State { get; set; }
        public string Message { get; set; }
    }
}