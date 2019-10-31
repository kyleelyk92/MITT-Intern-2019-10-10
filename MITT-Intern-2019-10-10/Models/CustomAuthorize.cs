using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MITT_Intern_2019_10_10.Models
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {//fix these
                    { "action", "Homepage" },
                    { "controller", "Home" },
                    { "parameterName", "YourParameterValue" }
                });
        }
    }
}