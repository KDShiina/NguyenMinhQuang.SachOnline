using NguyenMinhQuang.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenMinhQuang.SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        //DataClasses1DataContext db = new DataClasses1DataContext(@"Data Source=MINHQUANG;Initial Catalog=SachOnline;Integrated Security=True");
        DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: SachOnline
        public ActionResult Index()
        {
            var cd = from c in db.SACHes select c;

            return View(cd);
        }

        public ActionResult Search(string keyword)
        {
           var searchResults = db.SACHes.Where(book => book.TenSach.Contains(keyword) || book.MoTa.Contains(keyword)).ToList();

            return View(searchResults);
        }

        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in db.SACHes
                       where s.MaSach == id
                       select s;
            return View(sach.Single());

        }
    }
}