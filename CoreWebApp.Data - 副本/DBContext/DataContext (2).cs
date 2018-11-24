using CoreWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Data.DBContext
{
    public partial class DataContext : DbContext
    {
        public static string ConStr { get; set; } //连接字符串
        public DataContext(DbContextOptions<DataContext> options) :
            base(options)
        {
            //this.Configuration.AutoDetectChangesEnabled = false;
            //this.Configuration.ValidateOnSaveEnabled = false;
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }

        static DataContext()
        {
            ////一：数据库不存在时重新创建数据库
            //Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());

            ////二：每次启动应用程序时创建数据库
            //Database.SetInitializer<DataContext>(new DropCreateDatabaseAlways<DataContext>());

            ////三：模型更改时重新创建数据库
            //Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());

            ////四：从不创建数据库
            //Database.SetInitializer<DataContext>(null);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;database=NotifyBird;Trusted_Connection=True;");
        }

        public DbSet<User> User { get; set; }
    }
}
