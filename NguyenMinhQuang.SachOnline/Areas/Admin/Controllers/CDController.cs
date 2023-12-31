﻿using NguyenMinhQuang.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenMinhQuang.SachOnline.Areas.Admin.Controllers
{
    public class CDController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: Admin/CD
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public JsonResult DsChuDe()
        {
            try
            {
                var dsCD = (from cd in db.CHUDEs
                            select new
                            {
                                MaCD = cd.MaCD,
                                TenCD = cd.TenChuDe
                            }).ToList();
                return Json(new { code = 200, dsCD = dsCD, msg = "Lấy danh sách chủ đề thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Lấy danh sách chủ đề thất bại" + ex.Message
                },
                JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Detail(int maCD)
        {
            try
            {
                var cd = (from s in db.CHUDEs
                          where (s.MaCD == maCD)
                          select new
                          {
                              MaCD = s.MaCD,
                              TenChuDe = s.TenChuDe

                          }).SingleOrDefault();
                //db.CHUDES.SingleOrDefault(c => c.MaCD == maCD);
                return Json(new { code = 200, cd = cd, msg = "Lấy thông tin chủ đề thành công." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin chủ đề thất bại." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]

        public JsonResult AddChuDe(string strTenCD)
        {
            try
            {
                var cd = new CHUDE();
                cd.TenChuDe = strTenCD;
                db.CHUDEs.InsertOnSubmit(cd);
                db.SubmitChanges();
                return Json(new { code = 200, msg = "Thêm chủ đề thành công." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thêm chủ đề thất bại. LỖI " + ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }




        [HttpPost]

        public JsonResult Update(int maCD, string strTenCD)
        {
            try
            {
                var cd = db.CHUDEs.SingleOrDefault(c => c.MaCD == maCD);
                cd.TenChuDe = strTenCD;
                db.SubmitChanges();
                return Json(new { code = 200, msg = "sửa chủ đề thành công." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                {
                    return Json(new { code = 580, msg = "sửa chủ đề thất bại Lỗi " + ex.Message }, JsonRequestBehavior.AllowGet);


                }
            }


        }




        [HttpPost]

        public JsonResult Delete(int maCD)
        {
            try
            {
                var cd = db.CHUDEs.SingleOrDefault(c => c.MaCD == maCD);
                db.CHUDEs.DeleteOnSubmit(cd);
                db.SubmitChanges();
                return Json(new { code = 200, msg = "Xóa chủ đề thành công." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                {
                    return Json(new { code = 500, msg = "Xóa chủ đề thành công. Lỗi " + ex.Message }, JsonRequestBehavior.AllowGet);

                }
            }
        }

    }
}