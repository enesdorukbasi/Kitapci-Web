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
    public class KitapController : Controller
    {
        // GET: Yonetim/Kitap
        public ActionResult Listele()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return View(uow.KitapWork.GetAllWithTur());
            }
        }


        public ActionResult Ekle()
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                ViewBag.Turler = new SelectList(uow.TurWork.GetAll(), "Id", "Ad");
            }
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ekle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.KitapWork.Add(kitap);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                ViewBag.Turler = new SelectList(uow.TurWork.GetAll(), "Id", "Ad");
            }
            ModelState.AddModelError("", "Ekleme Başarısız.");
            return View(kitap);
        }


        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kitap kitap = uow.KitapWork.GetItem(id);
                    if (kitap != null)
                    {
                        ViewBag.Turler = new SelectList(uow.TurWork.GetAll(), "Id", "Ad");
                        return View(kitap);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            return RedirectToAction("Listele");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(int? id, Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.KitapWork.Update(kitap);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            using (UnitOfWork uow = new UnitOfWork())
            {
                ViewBag.Turler = new SelectList(uow.TurWork.GetAll(), "Id", "Ad");
            }
            ModelState.AddModelError("", "Düzenleme Yapılamadı.");
            return View(kitap);
        }


        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kitap kitap = uow.KitapWork.GetItem(id);
                    if (kitap != null)
                    {
                        return View(kitap);
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
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kitap kitap = uow.KitapWork.GetItem(id);
                    if (kitap != null)
                    {
                        uow.KitapWork.Remove(kitap);
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