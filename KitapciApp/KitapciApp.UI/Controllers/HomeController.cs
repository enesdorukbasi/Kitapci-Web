using KitapciApp.DataAccesLayer;
using KitapciApp.EntityLayer;
using KitapciApp.EntityLayer.SepetClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitapciApp.UI.Controllers
{
    public class HomeController : Controller
    {
        public string EPosta;
        //Kitap İşlemleri
        public ActionResult HomeView()
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                return View(uow.KitapWork.GetAllWithTur());
            }
        }

        public ActionResult KitapDetay(int? id)
        {
            ViewBag.secilenId = id;
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Kitap kitap = uow.KitapWork.GetItem(id);
                    ViewBag.secilenAd = kitap.Ad;
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
            return RedirectToAction("HomeView");
        }


        //Satış İşlemleri
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

            return RedirectToAction("HomeView");
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
                using (UnitOfWork uow = new UnitOfWork())
                {
                    ViewBag.kullanici = TempData["kullanici"];
                    sepet.Satıs.EPosta = ViewBag.kullanici;
                    sepet.Satıs.TarihSaat = DateTime.Now;
                    uow.SatısWork.Add(sepet.Satıs);
                    foreach (var item in sepet.Satıs.Detaylar)
                    {
                        Kitap kitap = uow.KitapWork.GetItem(item.Kitap.Id);
                        kitap.Adet -= item.Adet;
                        uow.KitapWork.Update(kitap);
                    }
                    uow.Save();
                    Session["sepet"] = null;
                }
            }
            return RedirectToAction("HomeView");
        }

        public ActionResult Kitaplarim()
        {
            Satıs satıs = new Satıs();
            Kullanıcı kullanıcı = new Kullanıcı();
            ViewBag.kullanici = TempData["kullanici"];
            using (UnitOfWork uow = new UnitOfWork())
            {
                return View(uow.SatısWork.GetAllWithKullanici(ViewBag.kullanici));
            }
        }

        public ActionResult KitaplarimDetay(int? id)
        {
            if (id != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    return View(uow.SatısDetayWork.GetAllWithKitap((int)id));
                }
            }
            return RedirectToAction("HomeView");
        }

        //Kullanıcı İşlemleri
        public ActionResult Login()
        {
            if (Session["user"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("HomeView", "Home");
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(Kullanıcı kullanıcı)
        {
            if (kullanıcı != null)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (uow.KullanıcıWork.Login(kullanıcı.EPosta.ToLower(), kullanıcı.Parola))
                    {
                        Kullanıcı user = uow.KullanıcıWork.GetItem(kullanıcı.EPosta);
                        if (user.Yetki == Yetkiler.Uye)
                        {
                            EPosta = kullanıcı.EPosta;
                            ViewBag.kullanici = user.EPosta;
                            TempData["kullanici"] = ViewBag.kullanici;
                            Session["user"] = user;
                            return RedirectToAction("HomeView", "Home");
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Kullanıcı adı veya parola hatalı.");
            return View(kullanıcı);
        }

        public ActionResult Logout()
        {
            if (Session["user"] != null)
            {
                Session.Remove("user");
            }
            return RedirectToAction("HomeView");
        }

        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ekle(Kullanıcı kullanıcı)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    kullanıcı.Yetki = Yetkiler.Uye;
                    uow.KullanıcıWork.Add(kullanıcı);
                    uow.Save();
                    return RedirectToAction("HomeView");
                }
            }
            return View(kullanıcı);
        }


    }
}