using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication2.App_Start;

namespace WebApplication2.Filters
{
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var uniquePageId = string.Empty;
            if (filterContext.HttpContext.Request.QueryString["tabId"] != null)
            {
                uniquePageId = filterContext.HttpContext.Request.QueryString["tabId"].ToString();
            }
            if (uniquePageId != null && uniquePageId != string.Empty)
            {
                if (!filterContext.HttpContext.Request.Headers.AllKeys.Any(x => x.ToUpper() == ("X-Custom-UniquePageId").ToUpper()))
                {
                    filterContext.HttpContext.Request.Headers.Add("X-Custom-UniquePageId", uniquePageId);
                }
                else
                {
                    var headerPageId = System.Web.HttpContext.Current.Request.Headers.GetValues("X-Custom-UniquePageId").FirstOrDefault();
                    if (headerPageId != null && headerPageId.ToString() != string.Empty && headerPageId.ToString() != uniquePageId)
                    {
                        filterContext.HttpContext.Request.Headers.Add("X-Custom-UniquePageId", uniquePageId);
                    }
                }
            }
        }
    }
}