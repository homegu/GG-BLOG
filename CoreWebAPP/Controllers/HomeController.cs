using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreWebAPP.Models;
using CoreWebApp.Data.DBContext;
using CoreWebApp.Data;
using CoreWebApp.Data.Extensions;

namespace CoreWebAPP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            //var user =  DbHelper.GetDataTable("select * from user", System.Data.CommandType.Text);

            //using (var db = new DataContext())
            //{
            //    User user = new User();
            //    user.UserName = "谷城";
            //    user.PassWord = "abcd1234";
            //    user.Address = "广东省深圳市龙华新区";
            //    db.User.Add(user);

            //    db.SaveChanges();
            //}
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
