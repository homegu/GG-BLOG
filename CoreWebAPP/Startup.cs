using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Repository.EF;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using CoreWebApp.Data.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreWebAPP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
   
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //配置EF的服务注册
            services.AddDbContext<DataContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("Default"), //读取配置文件中的链接字符串
                //    b => b.UseRowNumberForPaging());  //配置分页 使用旧方式
                options.UseSqlServer(Configuration.GetConnectionString("Default"));//读取配置文件中的链接字符串
            });

            DataContext.ConStr = Configuration.GetConnectionString("Default");
            services.AddScoped<DbContext>(provider => provider.GetService<DataContext>());

            //AddTransient瞬时模式：每次请求，都获取一个新的实例。即使同一个请求获取多次也会是不同的实例
            //AddScoped：每次请求，都获取一个新的实例。同一个请求获取多次会得到相同的实例
            //AddSingleton单例模式：每次都获取同一个实例
            //services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));

            services.AddCors(); //注册的 CORS 服务

            //Autofac 
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var module = new ConfigurationModule(Configuration);
            builder.RegisterModule(module);
            this.Container = builder.Build();

            return new AutofacServiceProvider(this.Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,DataContext context)
        {

            //app.UseCors(builder => builder.WithOrigins("http://localhost:65062")
            //                      .AllowAnyHeader().AllowAnyMethod());
            //启用CORS
            app.UseCors(builder => builder.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            DbInitializer.Initialize(context);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
