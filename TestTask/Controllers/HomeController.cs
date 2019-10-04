using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        private MessageContext db;
        User NewUser;
        public HomeController(MessageContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            if (Request.Cookies["GUID"] == null)
            {
                NewUser = new User { GUID = (db.Users.Max(c => c.Id) + 1) };
                db.Users.Add(NewUser);
                Response.Cookies.Append("GUID", NewUser.GUID.ToString(), new Microsoft.AspNetCore.Http.CookieOptions() { IsEssential = true });
                db.SaveChanges();
                ViewBag.GUID = NewUser.GUID.ToString();// ненастроеный GUID - говно, UserID отличается от Guid - либо искать свой userid либо сменить ключ связи таблицы на guid
            }
            else { ViewBag.GUID = Request.Cookies["GUID"]; }
            return View();
        }
        public IActionResult AddMessage(string message) 
        {
            Message n = new Message
            {
                UserId = Convert.ToInt32(Request.Cookies["GUID"]),
                Text = message
            };
            db.Messages.Add(n);
            db.SaveChanges();
            return PartialView(n);
        }
        
        [HttpPost]
        public IActionResult GetMessages(string submitButton)
        {
            List<Message> messag;
            switch (submitButton)
            {
                case "all":
                    {
                        messag = db.Messages.ToList();
                        return PartialView(messag);
                        break;
                    }
                case "user":
                    {
                        messag = db.Messages.Where(c => (c.UserId == Convert.ToInt32(Request.Cookies["GUID"]))).ToList();
                        return PartialView(messag);
                        break;
                    }
                default: break;
            }
            return View();
            db.Messages.OrderBy(e => e.UserId).Skip(100 - 10).Take(10);
        }
            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
