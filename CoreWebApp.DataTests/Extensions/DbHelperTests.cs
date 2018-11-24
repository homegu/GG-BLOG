using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreWebApp.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApp.Data.Extensions.Tests
{
    [TestClass()]
    public class DbHelperTests
    {
        [TestMethod()]
        public void ExecuteSqlCommandTest()
        {
            using (var db = new DataContext())
            {
                //User user = new User();
                //user.UserName = "gg";
                //user.PassWord = "gg123";
                //db.User.Add(user);

                //User user1 = new User();
                //user1.UserName = "请问请问";
                //user1.PassWord = "gucheng123123";
                //db.User.Add(user1);

                db.SaveChanges();

                var userList = db.User.ToList();
            }
            //DbHelper.ExecuteSqlCommand();
            Assert.Fail();
        }
    }
}