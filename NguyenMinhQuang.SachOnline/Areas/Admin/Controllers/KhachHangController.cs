using NguyenMinhQuang.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenMinhQuang.SachOnline.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        DataClasses1DataContext db = new DataClasses1DataContext();
        public KHACHHANG Lay1KH(int id)
        {
            return db.KHACHHANGs.SingleOrDefault(kh => kh.MaKH == id);

        }

        public ActionResult IndexKhachHang()
        {
            return View(db.KHACHHANGs);
        }

        public ActionResult Details(int id)
        {
            return View(Lay1KH(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(Lay1KH(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                var kh = Lay1KH(int.Parse(Request.Form["MaKH"]));
                kh.HoTen = Request.Form["HoTen"];
                kh.TaiKhoan = Request.Form["TaiKhoan"];
                kh.MatKhau = Request.Form["MatKhau"];
                kh.Email = Request.Form["Email"];
                kh.DiaChi = Request.Form["DiaChi"];
                kh.DienThoai = Request.Form["DienThoai"];
                DateTime ngaySinh;
                if (DateTime.TryParse(Request.Form["NgaySinh"], out ngaySinh))
                {
                    kh.NgaySinh = ngaySinh;
                }
                else
                {
                    ModelState.AddModelError("NgaySinh", "Ngày sinh không hợp lệ");
                    return View(kh);
                }

                db.SubmitChanges();
                return RedirectToAction("IndexKhachHang");
            }
            else
            {
                return RedirectToAction("IndexKhachHang");
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection f, KHACHHANG kh)
        {
            var sHoTen = f["HoTen"];
            var sTaiKhoan = f["TaiKhoan"];
            var sMatKhau = f["MatKhau"];
            var sMatKhauNhapLai = f["MatKhauNL"];
            var sDiaChi = f["DiaChi"];
            var sEmail = f["Email"];
            var sDienThoai = f["DienThoai"];
            var dNgaySinh = string.Format("{0:MM/dd/yyyy}", f["NgaySinh"]);
            if (db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại ";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "email đã được sử dụng ";
            }
            else if (ModelState.IsValid)
            {
                kh.HoTen = sHoTen;
                kh.TaiKhoan = sTaiKhoan;
                kh.Email = sEmail;
                kh.MatKhau = sMatKhau;
                kh.Email = sEmail;
                kh.DiaChi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("IndexKhachHang");
            }
            return RedirectToAction("IndexKhachHang");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(Lay1KH(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(KHACHHANG kh, int id)
        {
            using (var db = new DataClasses1DataContext())
            {
                var khToDelete = db.KHACHHANGs.SingleOrDefault(KH => KH.MaKH == id);
                if (khToDelete != null)
                {
                    db.KHACHHANGs.DeleteOnSubmit(khToDelete);
                    db.SubmitChanges();
                }
            }

            return RedirectToAction("IndexKhachHang");
        }
    }
}