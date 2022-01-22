using KitapciApp.DataAccesLayer;
using KitapciApp.EntityLayer;
using KitapciApp.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace KitapciApp.UI.Areas.Yonetim.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Yonetim/Kullanici
        public ActionResult Login()
        {
            if (Session["user"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("index", "DashBoard");
            }
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Login(Kullanıcı kullanıcı)
        {
            if (kullanıcı != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (uow.KullanıcıWork.Login(kullanıcı.EPosta.ToLower(), kullanıcı.Parola))
                    {
                        Kullanıcı user = uow.KullanıcıWork.GetItem(kullanıcı.EPosta);
                        if (user.Yetki == Yetkiler.Yonetici)
                        {
                            Session["user"] = user;
                            return RedirectToAction("index", "DashBoard");
                        }
                    }
                } 
            }
            ModelState.AddModelError("", "Kullanıcı adı veya parola hatalı.");
            return View(kullanıcı);
        }


        public ActionResult Logout()
        {
            if(Session["user"] != null)
            {
                Session.Remove("user");
            }
            return RedirectToAction("Login");
        }


        [Kimlik, Yetki(Yetki = Yetkiler.Yonetici)]
        public ActionResult Listele()
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                return View(uow.KullanıcıWork.GetAll());
            }
        }


        [Kimlik, Yetki(Yetki = Yetkiler.Yonetici)]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        [Kimlik, Yetki(Yetki = Yetkiler.Yonetici)]
        public ActionResult Ekle(Kullanıcı kullanıcı)
        {
            if (ModelState.IsValid)
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    uow.KullanıcıWork.Add(kullanıcı);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            return View(kullanıcı);
        }


        [Kimlik, Yetki(Yetki = Yetkiler.Yonetici)]
        public ActionResult Duzenle(string eposta)
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                Kullanıcı kullanıcı = uow.KullanıcıWork.GetItem(eposta);
                if(kullanıcı != null)
                {
                    return View(kullanıcı);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost,ValidateAntiForgeryToken]
        [Kimlik, Yetki(Yetki = Yetkiler.Yonetici)]
        public ActionResult Duzenle(Kullanıcı kullanıcı)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.KullanıcıWork.Update(kullanıcı);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            return View(kullanıcı);
        }


        [Kimlik, Yetki(Yetki = Yetkiler.Yonetici)]
        public ActionResult Sil(string eposta)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Kullanıcı kullanıcı = uow.KullanıcıWork.GetItem(eposta);
                if (kullanıcı != null)
                {
                    return View(kullanıcı);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost,ValidateAntiForgeryToken,ActionName("Sil")]
        [Kimlik, Yetki(Yetki = Yetkiler.Yonetici)]
        public ActionResult SilOnay(string eposta, Kullanıcı model)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Kullanıcı kullanıcı = uow.KullanıcıWork.GetItem(model.EPosta);
                if (kullanıcı != null)
                {
                    uow.KullanıcıWork.Remove(kullanıcı);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
    }
}