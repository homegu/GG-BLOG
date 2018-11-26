using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CoreWebApp.Data.DBContext;
using CoreWebApp.Data;
using CoreWebApp.Core;
using CoreWebApp.Model;

namespace Account.Repository.EF
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();
            //EnsureCreated()的作用是，如果有数据库存在，那么什么也不会发生。但是如果没有，那么就会创建一个数据库。
            //我们的Model不断再改变，数据库应该也随之改变，而EnsureCreated()就不够了，这就需要迁移 Migration

            if (context.Set<User>().Any())
            {
                var initUser = context.User.Where(x => x.UserName == "admin");
                if (initUser.IsNullOrEmpty())
                {
                    User initModel = new User
                    {
                        UserName = "admin",
                        PassWord = "admin",
                        Enabled = true,
                    };
                    context.User.Add(initModel);
                    context.SaveChanges();
                }
                return;
            }
        }
    }
}
