using KitapciApp.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitapciApp.UI.Filters
{
    public class YetkiAttribute : FilterAttribute, IAuthorizationFilter
    {
        public Yetkiler Yetki { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] != null)
            {
                Kullanıcı user = filterContext.HttpContext.Session["user"] as Kullanıcı;
                if (user != null)
                {
                    if (user.Yetki == Yetki)
                    {
                        return;
                    }
                }
            }
            filterContext.Result = new ViewResult()
            {
                ViewName = "YetkisizErisim"
            };
        }
    }
}