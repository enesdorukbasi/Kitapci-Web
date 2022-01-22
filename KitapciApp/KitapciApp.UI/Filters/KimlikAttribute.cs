using KitapciApp.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace KitapciApp.UI.Filters
{
    public class KimlikAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] != null)
            {
                Kullanıcı user = filterContext.HttpContext.Session["user"] as Kullanıcı;
                if (user != null)
                {
                    return;
                }
            }

            filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result != null && filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller", "Kullanici" },
                        {"action", "Login" }
                    }
                    );
            }
        }
    }
}