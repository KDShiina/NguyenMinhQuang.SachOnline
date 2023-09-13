using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using NguyenMinhQuang.SachOnline.Models;
using static NguyenMinhQuang.SachOnline.Models.DataClasses1DataContext;

namespace NguyenMinhQuang.SachOnline.Areas.Admin.Controllers
{
    public class FileAndMailController : Controller
    {
        // GET: Admin/FileAndMail
        [HttpGet]
        public ActionResult SendMail()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SendMail(Mail model)
        {
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {

                Credentials = new NetworkCredential("shiinayuki899@gmail.com", "yqawcqmducoivabm"),
                EnableSsl = true
            };

            var message = new MailMessage();
            message.From = new MailAddress(model.From);
            message.ReplyToList.Add(model.From);
            message.To.Add(new MailAddress(model.To)); message.Subject = model.Subject; message.Body = model.Notes;
            var f = Request.Files["Attachment"];
            var path = Path.Combine(Server.MapPath("~/UploadFile/"), f.FileName);
            if (!System.IO.File.Exists(path))
            {
                f.SaveAs(path);
            }
            Attachment data = new Attachment(Server.MapPath("~/UploadFile/" + f.FileName), MediaTypeNames.Application.Octet); message.Attachments.Add(data);
            mail.Send(message);
            return View("SendMail");

        }
    }
}