using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BaiTap
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "TrangTin",
                url: "{metatitle}",
                defaults: new { controller = "SachOnline", action = "TrangTin" },
                namespaces: new string[] { "NguyenMinhQuang.SachOnline.Controllers" }
            );*/

          

            routes.MapRoute(
               name: "Trang chu",
               url: "",
               defaults: new { controller = "SachOnline", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "NguyenMinhQuang.SachOnline.Controllers" }
           );
            routes.MapRoute(
                name: "Trang tin",
                url: "{metatitle}",
                defaults: new { controller = "SachOnline", action = "TrangTin", metatitle = UrlParameter.Optional },
                namespaces: new string[] { "SachOnline.Controllers" }
            );
            routes.MapRoute(
                name: "Sach theo Chu de",
                url: "sach-theo-chu-de-{maCD}",
                defaults: new { controller = "SachOnline", action = "SachTheoCD", MaCD = UrlParameter.Optional },
                namespaces: new string[] { "NguyenMinhQuang.SachOnline.Controllers" }
            );

            routes.MapRoute(
               name: "Sach theo NXB",
               url: "sach-theo-nxb-{maNXB}",
               defaults: new { controller = "SachOnline", action = "SachTheoNXB", MaNXB = UrlParameter.Optional },
               namespaces: new string[] { "NguyenMinhQuang.SachOnline.Controllers" }
           );

            routes.MapRoute(
                name: "Chi tiet sach",
                url: "chi-tiet-sach-{id}",
                defaults: new { controller = "SachOnline", action = "ChiTietSach", MaSach = UrlParameter.Optional },
                namespaces: new string[] { "NguyenMinhQuang.SachOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Dang ky",
                url: "dang-ky",
                defaults: new { controller = "User", action = "DangKy" },
                namespaces: new string[] { "NguyenMinhQuang.SachOnline.Controllers" }
            );
            routes.MapRoute(
                name: "Dang nhap",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "DangNhap", url = UrlParameter.Optional },
                namespaces: new string[] { "NguyenMinhQuang.SachOnline.Controllers" }
            );
            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "SachOnline", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "NguyenMinhQuang.SachOnline.Controllers" }
          );
        }
    }
}
