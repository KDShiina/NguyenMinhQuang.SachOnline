using System;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using NguyenMinhQuang.SachOnline.Models;
using System.Net;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NguyenMinhQuang.SachOnline.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: Admin/DonHang
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 10; // You can adjust the page size as needed.
            var orders = db.DONDATHANGs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(iPageNum, iPageSize);
            return View(orders);
        }

        // GET: Admin/DonHang/Details/5
        public ActionResult Details(int id)
        {
            var order = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy thông tin chi tiết đơn hàng từ cơ sở dữ liệu
            var orderDetails = db.CHITIETDATHANGs.Where(d => d.MaDonHang == id);

            ViewBag.OrderDetails = orderDetails; // Truyền thông tin chi tiết vào ViewBag

            return View(order);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (order == null)
            {
                return HttpNotFound();
            }

            try
            {
                // Handle order deletion and related tables as needed.
                // You can add the necessary logic here.

                db.DONDATHANGs.DeleteOnSubmit(order);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle the exception and set an error message
                ModelState.AddModelError(string.Empty, "Error deleting order. Please try again.");

                // Return the "Delete" view with the order model
                return View("Delete", order);
            }
        }
        public DONDATHANG Lay1CD(int maCD)
        {
            return db.DONDATHANGs.Where(nxb => nxb.MaDonHang == maCD).SingleOrDefault();
        }

        [HttpGet]
        public ActionResult SuaCD()
        {
            var maCD = Request.QueryString["MaDonHang"];
            return View("SuaCD", Lay1CD(int.Parse(maCD)));
        }


        [HttpPost]
        public ActionResult SuaCD(FormCollection f)
        {
            var maCD = Lay1CD(int.Parse(f["MaDonHang"]));
            maCD.TinhTrangGiaoHang = int.Parse(f["TinhTrangGiaoHang"]);
            maCD.DaThanhToan = bool.Parse(f["DaThanhToan"]);
            maCD.NgayDat = Convert.ToDateTime(f["NgNgayDataySinh"]);
            maCD.NgayGiao = Convert.ToDateTime(f["NgayGiao"]);
            maCD.MaKH = int.Parse(f["MaKH"]);


            db.SubmitChanges();
            return RedirectToAction("Index");
        }


    }
}
