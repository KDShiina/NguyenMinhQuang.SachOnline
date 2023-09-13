using NguyenMinhQuang.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTap.Controllers
{
    public class SachOnlineController : Controller
    {
        //DataClasses1DataContext db = new DataClasses1DataContext(@"Data Source=MINHQUANG;Initial Catalog=SachOnline;Integrated Security=True");
        DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: SachOnline
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string keyword)
        {
           var searchResults = db.SACHes.Where(book => book.TenSach.Contains(keyword) || book.MoTa.Contains(keyword)).ToList();

            return View(searchResults);
        }




    }
}