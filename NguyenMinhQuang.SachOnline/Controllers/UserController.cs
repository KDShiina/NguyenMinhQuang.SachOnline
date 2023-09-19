using NguyenMinhQuang.SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenMinhQuang.SachOnline.Controllers
{
    
    public class UserController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: User
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            if (string.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn Chưa Nhập Tên Tài Khoản ";
            }
            else if (string.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Bạn Chưa Nhập Mật Khẩu  ";
            }
            else
            {
                // truy vẫn dữ liệu trong sql

                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN && n.MatKhau == sMatKhau);
                if (kh != null)
                {
                    // HIỆN THÔNG  BÁO 
                    ViewBag.ThongBao = "ĐĂNG NHẬP THÀNH CÔNG ";
                    Session["TaiKhoan"] = kh;
                    Session["HoTen"] = kh.HoTen;
                    return RedirectToAction("Index","SachOnline");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu sai ";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult chiTietKH()
        {
            var khachHang = Session["TaiKhoan"] as KHACHHANG;

            if (khachHang == null)
            {
                return HttpNotFound();
            }

            return View(khachHang);

        }

        public ActionResult DangXuat()
        {
            Session.Remove("TaiKhoan");
            return RedirectToAction("DangNhap","User");

        }
    }
}