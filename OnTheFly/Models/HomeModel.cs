using LNF.Control;
using LNF.Repository.Control;
using LNF.Repository.Scheduler;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace OnTheFly.Models
{
    public class HomeModel
    {
        public string CurrentDisplayName { get; set; }

        public string LogInUrl { get; set; }

        public IEnumerable<OnTheFlyResourceItem> OnTheFlyResources { get; set; }

        public class OnTheFlyResourceItem
        {
            public OnTheFlyResource Resource { get; set; }
            public bool? State { get; set; }

            public IHtmlString GetState()
            {
                if (!State.HasValue)
                {
                    return Span("???", new { @class = "error" });
                }
                else
                {
                    if (State.Value)
                        return Span("ON", new { @class = "state-on" });
                    else
                        return Span("OFF", new { @class = "state-off" });
                }
            }

            public IHtmlString GetResourceID()
            {
                if (Resource.Resource == null)
                    return Span("???", new { @class = "error" });
                else
                    return new HtmlString(Resource.Resource.ResourceID.ToString());
            }

            public IHtmlString GetResourceName()
            {
                if (Resource.Resource == null)
                {
                    return Span("???", new { @class = "error" });
                }
                else
                    return new HtmlString(Resource.Resource.ResourceName);
            }

            private IHtmlString Span(string text, object htmlAttributes = null)
            {
                TagBuilder builder = new TagBuilder("span");

                builder.SetInnerText(text);

                if (htmlAttributes != null)
                {
                    var attr = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                    foreach (var kvp in attr)
                        builder.Attributes.Add(kvp.Key, kvp.Value.ToString());
                }

                return new HtmlString(builder.ToString());
            }
        }
    }
}