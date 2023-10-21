using NguyenMinhQuang.SachOnline.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenMinhQuang.SachOnline.Areas.Admin.Controllers
{
    public class SachController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: Admin/Sach
        public ActionResult IndexAdmin(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.SACHes.ToList().OrderBy(n=>n.MaSach).ToPagedList(iPageNum,iPageSize));
        }
    }
}