using NguyenMinhQuang.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using static NguyenMinhQuang.SachOnline.Models.DataClasses1DataContext;

namespace NguyenMinhQuang.SachOnline.Areas.Admin.Controllers
{
    public class NXBController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public NHAXUATBAN Lay1NXB(int id)
        {
            return db.NHAXUATBANs.SingleOrDefault(nxb => nxb.MaNXB == id);

        }
        // GET: Admin/NXB
        public ActionResult ChiTietNXB()
        {
            return View(db.NHAXUATBANs);
        }



        public ActionResult ChiTiet(int id)
        {
            return View(Lay1NXB(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(Lay1NXB(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                var nxb = Lay1NXB(int.Parse(Request.Form["MaNXB"]));
                nxb.TenNXB = Request.Form["TenNXB"];
                nxb.DiaChi = Request.Form["DiaChi"];
                nxb.DienThoai = Request.Form["DienThoai"];
                db.SubmitChanges();
                return RedirectToAction("ChiTietNXB");
            }
            else
            {
                return RedirectToAction("ChiTietNXB");
            }
        }

        [HttpGet]
        public ActionResult insert()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult insert(NHAXUATBAN nxb)
        {
            if (ModelState.IsValid)
            {
                db.NHAXUATBANs.InsertOnSubmit(nxb); // Add the new publisher
                db.SubmitChanges();
                return RedirectToAction("ChiTietNXB");
            }

            return View(nxb);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(Lay1NXB(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(NHAXUATBAN nxb, int id)
        {
            using (var db = new DataClasses1DataContext())
            {
                var nxbToDelete = db.NHAXUATBANs.SingleOrDefault(NXB => NXB.MaNXB == id);
                if (nxbToDelete != null)
                {
                    db.NHAXUATBANs.DeleteOnSubmit(nxbToDelete);
                    db.SubmitChanges();
                }
            }

            return RedirectToAction("ChiTietNXB");
        }


    }
}