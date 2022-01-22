using KitapciApp.DataAccesLayer;
using KitapciApp.EntityLayer;
using KitapciApp.EntityLayer.SepetClasses;
using KitapciApp.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitapciApp.UI.Areas.Yonetim.Controllers
{
    [Kimlik, Yetki(Yetki = Yetkiler.Yonetici)]
    public class SatisController : Controller
    {
        // GET: Yonetim/Satis
        public ActionResult Listele()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return View(uow.SatısWork.GetAll());
            }
        }

        public ActionResult SatisDetay(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    return View(uow.SatısDetayWork.GetAllWithKitap((int)id));
                }
            }
            return RedirectToAction("Listele");
        }


        public ActionResult Sil(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Satıs satıs = uow.SatısWork.GetItem(id);
                    if (satıs != null)
                    {
                        return View(satıs);
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
                    Satıs satıs = uow.SatısWork.GetItem(id);
                    if (satıs != null)
                    {
                        uow.SatısWork.Remove(satıs);
                        uow.Save();
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            return RedirectToAction("Listele");
        }

        public ActionResult Duzenle(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Satıs satıs = uow.SatısWork.GetItem(id);
                    if (satıs != null)
                    {
                        return View(satıs);
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
        public ActionResult Duzenle(int? id, Satıs satıs)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.SatısWork.Update(satıs);
                    uow.Save();
                    return RedirectToAction("Listele");
                }
            }
            ModelState.AddModelError("", "Düzenleme Yapılamadı.");
            return View(satıs);
        }

        public ActionResult SatisEkle()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return View(uow.KitapWork.GetAllWithTur());
            }
        }

        public ActionResult SepeteEkle(int? id)
        {
            if (id != null)
            {
                Sepet sepet;
                if (Session["sepet"] != null)
                {
                    sepet = Session["sepet"] as Sepet;
                }
                else
                {
                    sepet = new Sepet();
                }
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kullanıcı kullanıcı = new Kullanıcı();
                    ViewBag.kullanici = TempData["kullanici"];
                    kullanıcı.EPosta = ViewBag.kullanici;
                    string eposta = kullanıcı.EPosta;
                    Kitap kitap = uow.KitapWork.GetItem(id);
                    if (kitap != null)
                    {
                        sepet.Ekle(kitap, eposta);
                        Session["sepet"] = sepet;
                    }
                }

            }

            return RedirectToAction("SatisEkle");
        }

        public ActionResult SepetGoster()
        {
            Sepet sepet;
            if (Session["sepet"] != null)
            {
                sepet = Session["sepet"] as Sepet;
            }
            else
            {
                sepet = new Sepet();
            }
            return View(sepet.Satıs);
        }

        public ActionResult SatisTamamla()
        {
            Sepet sepet;
            if (Session["sepet"] != null)
            {
                sepet = Session["sepet"] as Sepet;
                using(UnitOfWork uow = new UnitOfWork())
                {
                    sepet.Satıs.TarihSaat = DateTime.Now;
                    uow.SatısWork.Add(sepet.Satıs);
                    foreach(var item in sepet.Satıs.Detaylar)
                    {
                        Kitap kitap = uow.KitapWork.GetItem(item.Kitap.Id);
                        kitap.Adet -= item.Adet;
                        uow.KitapWork.Update(kitap);
                    }
                    uow.Save();
                    Session["sepet"] = null;
                }
            }
            return RedirectToAction("SatisEkle");
        }
    }
}