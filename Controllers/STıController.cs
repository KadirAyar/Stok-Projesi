using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stok_Projesi.Models;


namespace Stok_Projesi.Controllers
{
    public class STıController : Controller
    {
        StokEkstresiEntities db = new StokEkstresiEntities();

        // GET: Home
        public ActionResult Index(string search)
        {
            var src = from b in db.STı
                      select b;
            if (!String.IsNullOrEmpty(search))
            {
                src = src.Where(x => x.IslemTur.ToString().Contains(search) || x.MalKodu.Contains(search));

            }
            return View(src.ToList());
        }

      
        public ActionResult Create()
        {
            List<SelectListItem> deger1 = (from x in db.STK.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.MalAdi,
                                               Value = x.Id.ToString()
                                           }).ToList();
            ViewBag.ktgr = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult Create(STı data)
        {
           
            db.STı.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }
        public void ExportExcel()
        {
            var gv = new GridView()
            {
                DataSource = db.STı.OrderBy(x => x.Id).ToList()
            };
            
            gv.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition",
                string.Format("attachment;filename=STıListing_{0}.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            var strw = new StringWriter();
            var htmlTextWriter = new HtmlTextWriter(strw);
            gv.RenderControl(htmlTextWriter);
            Response.Write(strw.ToString());
            Response.End();
        }
     

    }
}