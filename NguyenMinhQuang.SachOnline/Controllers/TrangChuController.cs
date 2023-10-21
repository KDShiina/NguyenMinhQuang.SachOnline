﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenMinhQuang.SachOnline.Models;
namespace NguyenMinhQuang.SachOnline.Controllers
{
    public class TrangChuController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: TrangChu
        public ActionResult SachOnline()
        {
            return View();
        }

        public ActionResult ChuDePartial()
        {
            var cd = from c in db.CHUDEs select c;
            return PartialView(cd);
        }
        public ActionResult NBXPartial()
        {
            var cd = from c in db.NHAXUATBANs select c;

            return PartialView(cd);
        }
        public ActionResult NavPartial()
        {
            return PartialView();
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
        public ActionResult SliderPartial()
        {
            return PartialView();
        }
        public ActionResult SachBanNhieuPartial()
        {
            var sachBanNhieu = db.SACHes
                                .OrderByDescending(s => s.SoLuongBan)
                                .Take(6)
                                .ToList();

            return PartialView(sachBanNhieu);
        }

        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }

    }


}