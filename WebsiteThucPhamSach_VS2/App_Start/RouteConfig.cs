using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteThucPhamSach_VS2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            

            //Đăng xuất
            routes.MapRoute(
                name: "Đăng Xuất",
                url: "Dang-Xuat",
                defaults: new
                {
                    controller = "Header",
                    action = "Logout"
                }
            );
            //Đăng ký 
            routes.MapRoute(
                name: "Đăng Ký",
                url: "Dang-Ky",
                defaults: new
                {
                    controller = "Header",
                    action = "Register"
                }
            );
            //Đổi mật khẩu
            routes.MapRoute(
                name: "Đổi mật khẩu",
                url: "Doi-Mat-Khau",
                defaults: new
                {
                    controller = "Header",
                    action = "ChangePassword"
                }
            );
            //Quên mật khẩu
            routes.MapRoute(
                name: "Quên mật khẩu",
                url: "Quen-Mat-Khau",
                defaults: new
                {
                    controller = "Header",
                    action = "ForgotPassword"
                }
            );
            //Thông tin cá nhân
            routes.MapRoute(
                name: "Thông tin cá nhân",
                url: "Thong-Tin-Ca-Nhan",
                defaults: new
                {
                    controller = "Header",
                    action = "PersonalInformation"
                }
            );
            //Đăng nhập
            routes.MapRoute(
                name: "Đăng Nhập",
                url: "Dang-Nhap",
                defaults: new
                {
                    controller = "Header",
                    action = "Login"
                }
            );

            //Đăng nhập facebook
            routes.MapRoute(
                name: "Đăng nhập facebook",
                url: "Dang-Nhap-Facebook",
                defaults: new { controller = "Header", action = "LoginFacebook", id = UrlParameter.Optional },
                namespaces: new string[] { "WebsiteThucPhamSach.Controllers" }
            );
            //Liên hệ
            routes.MapRoute(
                name: "Liên hệ",
                url: "Lien-He",
                defaults: new
                {
                    controller = "Home",
                    action = "Contacts"
                }
            );
            //Tin tức
            routes.MapRoute(
                name: "Tin Tức",
                url: "Tin-Tuc",
                defaults: new
                {
                    controller = "Home",
                    action = "News"
                }
            );
            //Chi Tiết Tin tức
            routes.MapRoute(
                name: "Chi Tiết Tin Tức",
                url: "Chi-Tiet-Tin-Tuc/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "DetailNew",
                    id = UrlParameter.Optional
                }
            );
            //Chi Tiết Sản Phẩm
            routes.MapRoute(
                name: "Chi Tiết Sản Phẩm",
                url: "Chi-Tiet-San-Pham/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "DetailProduct",
                    id = UrlParameter.Optional
                }
            );
            //Sản Phẩm
            routes.MapRoute(
                name: "Sản Phẩm",
                url: "San-Pham/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Products",
                    id = UrlParameter.Optional
                }
            );
            //Sản Phẩm
            routes.MapRoute(
                name: "Danh Mục Trái",
                url: "Danh-Muc-Trai/{id}",
                defaults: new
                {
                    controller = "Menu",
                    action = "LeftChildMenusPartial",
                    id = UrlParameter.Optional
                }
            );
            //Giỏ hàng
            routes.MapRoute(
                name: "Giỏ Hàng",
                url: "Gio-Hang",
                defaults: new
                {
                    controller = "Home",
                    action = "Carts",
                    id = UrlParameter.Optional
                }
            );

            //Thanh toán
            routes.MapRoute(
                name: "Thanh Toán",
                url: "Thanh-Toan",
                defaults: new
                {
                    controller = "Home",
                    action = "Payment",
                    id = UrlParameter.Optional
                }
            );

            //Giới thiệu
            routes.MapRoute(
                name: "Giới Thiệu",
                url: "Gioi-Thieu",
                defaults: new
                {
                    controller = "Home",
                    action = "About"
                }
            );
            //Trang chủ
            routes.MapRoute(
                name: "Trang Chủ",
                url: "Trang-Chu",
                defaults: new
                {
                    controller = "Home",
                    action = "Index"
                }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
