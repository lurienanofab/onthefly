using LNF.Repository.Scheduler;
using System.Configuration;

namespace OnTheFly.Models
{
    public class ApiModel
    {
        public ApiModel()
        {
            ServerResponse = "-";
            ResourceType = OnTheFlyResourceType.Tool;
            ButtonPressWaitTime = int.Parse(ConfigurationManager.AppSettings["otf:ButtonPressWaitTime"]);
        }

        public string ServerResponse { get; set; }
        public OnTheFlyResourceType ResourceType { get; set; }
        public int ButtonPressWaitTime { get; set; }
    }
}