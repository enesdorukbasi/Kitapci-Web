using KitapciApp.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitapciApp.UI.Areas.Yonetim.Controllers
{
    [Kimlik]
    public class DashBoardController : Controller
    {
        // GET: Yonetim/DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}