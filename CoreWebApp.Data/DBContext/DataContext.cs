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


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    ///DomainMapping  所在的程序集一定要写对，因为目前在当前项目所以是采用的当前正在运行的程序集 如果你的mapping在单独的项目中 记得要加载对应的assembly
        //    ///这是重点
        //    var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
        //  .Where(type => !String.IsNullOrEmpty(type.Namespace))
        //  .Where(type => type.BaseType != null && type.BaseType.BaseType != null && type.BaseType.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
        //    foreach (var type in typesToRegister)
        //    {
        //        dynamic configurationInstance = Activator.CreateInstance(type);
        //        modelBuilder.Configurations.Add(configurationInstance);
        //    }

        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<User> User { get; set; }
    }
}
