using NguyenMinhQuang.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NguyenMinhQuang.SachOnline.Models.DataClasses1DataContext;

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

        public ActionResult Search(string keyword = null, int maCD = 0)
        {
            ViewBag.cd = db.CHUDEs.ToList();
            var searchResults = db.SACHes.AsQueryable();

            if (maCD != 0)
            {
                searchResults = searchResults.Where(book => book.CHUDE.MaCD == maCD);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                searchResults = searchResults.Where(book => book.TenSach.Contains(keyword));
            }

            return View(searchResults.ToList());
        }

        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in db.SACHes
                       where s.MaSach == id
                       select s;
            return View(sach.Single());

        }

        public ActionResult Group()
        {
            var kq = from s in db.SACHes group s by s.MaCD;
            //var kq = db.SACHes.GroupBy(s => s.MaCD);
            return View(kq);
        }

        public ActionResult ThongKe()
        {
            var kq = from s in db.SACHes
                     join cd in db.CHUDEs on s.MaCD equals cd.MaCD
                     group s by new { cd.MaCD, cd.TenChuDe } into g
                     select new ReportInfo
                     {
                         Id = g.Key.MaCD.ToString(), // Mã CD
                         Name = g.Key.TenChuDe,      // Tên CD
                         Count = g.Count(),
                         Sum = g.Sum(n => n.SoLuongBan),
                         Max = g.Max(n => n.SoLuongBan),
                         Min = g.Min(n => n.SoLuongBan),
                         Avg = Convert.ToDecimal(g.Average(n => n.SoLuongBan))
                     };
            return View(kq);
        }
    }
}

