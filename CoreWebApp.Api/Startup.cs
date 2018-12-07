using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using CoreWebApp.Infrastructure;
using CoreWebApp.Data.DBContext;
using CoreWebApp.Repository;
using CoreWebApp.Repository.Contract;
using CoreWebApp.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CoreWebApp.Data.DBContext.Init;

//jwt
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
//end jwt

using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;
using CoreWebApp.Api.Filter;
using CoreWebApp.Api.Middleware;
using log4net.Repository;
using log4net;
using log4net.Config;
using Blog.Core.Log;

namespace CoreWebApp.Api
{
    public class Startup
    {

        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository repository { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //log4net
            repository = LogManager.CreateRepository("CoreWebApp.Service");
            //指定配置文件
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => {
                options.Filters.Add<GlobalExceptionFilter>(); 
                //options.Filters.Add<ModelVerifyActionFilter>(); //自带模型验证，废弃
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMemoryCache();

            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

            //log日志注入
            services.AddSingleton<ILoggerHelper, LogHelper>();

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);

            //添加jwt验证：

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = jwtSettings.Audience,//Audience
                    ValidIssuer = jwtSettings.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),//拿到SecurityKey
                    ClockSkew = TimeSpan.FromSeconds(30)//注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟
                };
            });

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = $"博客接口文档",
                    Description = $"博客 HTTP API v1",
                    TermsOfService = "None",
                });
                //var basePath = AppContext.BaseDirectory;
                var xmlPath = System.IO.Path.Combine(basePath, "CoreWebApp.Api.xml");
                options.IncludeXmlComments(xmlPath);
            });

            #region CORS
            //跨域第二种方法，声明策略，记得下边app中配置
            services.AddCors(c =>
            {
                //↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
                c.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
                //↑↑↑↑↑↑↑注意正式环境不要使用这种全开放的处理↑↑↑↑↑↑↑↑↑↑


                //一般采用这种方法
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins("http://localhost:9528", "http://localhost:8080", "http://localhost:8021", "http://localhost:8081", "http://localhost:1818")//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });

            //跨域第一种办法，注意下边 Configure 中进行配置
            //services.AddCors();
            #endregion

            return new AutofacServiceProvider(AutofacCore.InitAutofac(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataContext context)
        {
            //身份验证接口自定义信息中间件
            //app.UseErrorHandling();
            //app.UseStatusCodePages();//把错误码返回前台，比如是404

            //注意此授权方法已经放弃，请使用下边的官方验证方法。但是如果你还想传User的全局变量，还是可以继续使用中间件
            //app.UseMiddleware<TokenAuth>();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "博客接口文档 v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //跨域第二种方法，使用策略，详细策略信息在ConfigureService中
            app.UseCors("LimitRequests");//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。

            //跨域第一种版本，请要ConfigureService中配置服务 services.AddCors(); 
            //    app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader()
            //.AllowAnyMethod());

            ////todo:测试可以允许任意跨域，正式环境要加权限
            //app.UseCors(builder => builder.AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader());


            DbInitializer.Initialize(context);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
