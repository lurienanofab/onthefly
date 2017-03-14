using LNF.Cache;
using LNF.CommonTools;
using LNF.Control;
using LNF.Repository;
using LNF.Repository.Control;
using LNF.Repository.Scheduler;
using OnTheFly.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace OnTheFly.Controllers
{
    public class HomeController : Controller
    {
        private Dictionary<int, BlockState> blocks = new Dictionary<int, BlockState>();

        public async Task<ActionResult> Index(HomeModel model)
        {
            model.CurrentDisplayName = CacheManager.Current.CurrentUser.DisplayName;
            model.LogInUrl = FormsAuthentication.LoginUrl;
            model.OnTheFlyResources = await GetResources();
            return View(model);
        }

        private async Task<IEnumerable<HomeModel.OnTheFlyResourceItem>> GetResources()
        {
            var result = new List<HomeModel.OnTheFlyResourceItem>();

            foreach (var res in DA.Current.Query<OnTheFlyResource>())
            {
                var state = await GetState(res);
                result.Add(new HomeModel.OnTheFlyResourceItem()
                {
                    Resource = res,
                    State = state
                });
            }

            return result;
        }

        private async Task<bool?> GetState(OnTheFlyResource res)
        {
            ActionInstance act = ActionInstanceUtility.Find(ActionType.Interlock, res.Resource.ResourceID);

            if (act == null)
                return null;

            var point = act.GetPoint();
            var blockState = await GetBlockState(point.Block.BlockID);
            var result = WagoInterlock.GetPointState(point.PointID, blockState);

            return result;
        }

        private async Task<BlockState> GetBlockState(int blockId)
        {
            BlockState result = null;

            if (blocks.ContainsKey(blockId))
                result = blocks[blockId];
            else
            {
                result = await WagoInterlock.GetBlockState(blockId);
                blocks.Add(blockId, result);
            }

            return result;
        }
    }
}