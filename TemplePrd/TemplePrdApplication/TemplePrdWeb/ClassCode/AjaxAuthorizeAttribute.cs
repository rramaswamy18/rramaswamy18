using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailAdmWeb.ClassCode
{
    public class AjaxAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                var urlHelper = new UrlHelper(context.RequestContext);
                context.HttpContext.Response.StatusCode = 403;
                context.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "NotAuthorized",
                        //LogOnUrl = urlHelper.Action("Index", "Home"),
                        ReturnUrl = HttpContext.Current.Request.Url.AbsolutePath,
                        UnauthorizedUrl = "Home/Unauthorized",
                        UrlAbsoluteUri = HttpContext.Current.Request.Url.AbsoluteUri,
                        UrlAbsolutePath = HttpContext.Current.Request.Url.AbsolutePath,
                        UrlHost = HttpContext.Current.Request.Url.Host,
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(context);
            }
        }
    }
}
