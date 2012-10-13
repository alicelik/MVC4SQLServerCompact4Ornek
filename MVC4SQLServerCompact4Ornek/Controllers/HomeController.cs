using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC4SQLServerCompact4Ornek.Models; // dbEntities ve diller sınıflarını barındırıyor.

namespace MVC4SQLServerCompact4Ornek.Controllers
{
    public class HomeController : Controller
    {
        dbEntities model = new dbEntities(); // bu tanım ile tüm metotlarımızda veritabanı işlemlerini gerçekleştirebiliriz

        public ActionResult Index()// Başlangıçta Index action'ı çalışır.
        {
            return View(model.diller); // Ve model.diller değişkeni kullanılmak üzere view katmanına gönderilir.
        }
        [HttpPost]
        public ActionResult DilEkle(string ad) // Post form metoduyla veriyi parametreye atar.
        {
            model.diller.Add(new diller() { ad = ad }); // model değişkenine ekleniyor.            
            model.SaveChanges(); // ve sdf uzantılı veritabanına değişiklikler gerçekleştiriliyor.
            return RedirectToAction("Index"); // Daha sonra Index action'ına yönleniyor.
        }
        public ActionResult DilDuzenle(int id) // id değişkeni route ile metoda geliyor.
        {
            diller dil = model.diller.Find(id); // id parametresi sayesinde ilgili satırı model değişkeninden çekiyoruz.
            return View(dil); // Değişkeni view katmanına gönderiyoruz.
        }
        [HttpPost]
        public ActionResult DilDuzenle(int id, string ad) // Post form metodu ile veriler parametreye veriliyor.
        {
            diller dil = model.diller.Find(id); // id ile select işlemi yapılıyor.
            model.Entry(dil).State = EntityState.Modified; // Entity durumunu Modified olarak tanımladığımızda kod blogunda  bundan sonraki işlemler update komutuyla yapılacaktır.
            dil.ad = ad; // Burada dil değişkeninin ad özelliğine parametreyi atıyoruz.
            model.SaveChanges(); // Veritabanı üzerinde değişiklikler yapılıyor.
            return RedirectToAction("Index"); // ilk açılan Index action'ına yönleniyor.
        }
        public ActionResult DilSil(int id)
        {
            diller dil = model.diller.Find(id); // id ile select işlemi yapılıyor.
            model.Entry(dil).State = EntityState.Deleted; // Entity durumunu Deleted olarak tanımladığımızda kod blogunun bitinceye kadar arka tarafta delete komutu yorumlanacaktır.
            model.SaveChanges(); // Veritabanı üzerinde değişiklikler yapılıyor.
            return RedirectToAction("Index"); // Index action'ına yönleniyor.
        }
    }
}
