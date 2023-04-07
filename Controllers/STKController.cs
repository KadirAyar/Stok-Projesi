using Stok_Projesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Stok_Projesi.Controllers
{
    public class STKController : Controller
    {
        StokEkstresiEntities db = new StokEkstresiEntities();
        // GET: STK

        public ActionResult Index()
        {
            var list = db.STK.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(STK data)
        {
            db.STK.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}