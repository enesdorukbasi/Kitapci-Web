using KitapciApp.DataAccesLayer;
using KitapciApp.EntityLayer;
using KitapciApp.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitapciApp.UI.Areas.Yonetim.Controllers
{
    [Kimlik, Yetki(Yetki = Yetkiler.Yonetici)]
    public class TurController : Controller
    {
        // GET: Yonetim/Tur
        public ActionResult Listele()
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                return View(uow.TurWork.GetAll());
            }
        }


        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ekle(Tur tur)
        {
            if (ModelState.IsValid)
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    uow.TurWork.Add(tur);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            ModelState.AddModelError("", "Ekleme Başarısız.");
            return View(tur);
        }


        public ActionResult Duzenle(int? id)
        {
            if(id != null)
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    Tur tur = uow.TurWork.GetItem(id);
                    if(tur != null)
                    {
                        return View(tur);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            return RedirectToAction("Listele");
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Duzenle(int? id, Tur tur)
        {
            if (ModelState.IsValid)
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    uow.TurWork.Update(tur);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            ModelState.AddModelError("", "Düzenleme Yapılamadı.");
            return View(tur);
        }


        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Tur tur = uow.TurWork.GetItem(id);
                    if (tur != null)
                    {
                        return View(tur);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            return RedirectToAction("Listele");
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Sil")]
        public ActionResult SilOnay(int? id)
        {
            if(id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Tur tur = uow.TurWork.GetItem(id);
                    if (tur != null)
                    {
                        uow.TurWork.Remove(tur);
                        uow.Save();
                        return RedirectToAction("Listele");
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            return RedirectToAction("Listele");
        }
    }
}