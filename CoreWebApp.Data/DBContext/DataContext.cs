using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApp.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext() :
            base("default")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        static DataContext() {
            //不要让EF修改数据库模式
            //Database.SetInitializer<DataContext>(null);

            ////一：数据库不存在时重新创建数据库
            //Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());

            ////二：每次启动应用程序时创建数据库
            //Database.SetInitializer<DataContext>(new DropCreateDatabaseAlways<DataContext>());
    
            ////三：模型更改时重新创建数据库
            //Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());
        
            ////四：从不创建数据库
            //Database.SetInitializer<DataContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }
    }
}
