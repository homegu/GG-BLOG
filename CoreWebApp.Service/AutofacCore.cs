using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using CoreWebApp.Repository;
using CoreWebApp.Repository.Contract;
using CoreWebApp.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using IContainer = Autofac.IContainer;

namespace CoreWebApp.Service
{
    public static class AutofacCore
    {
        private static IContainer _container;
        public static IContainer InitAutofac(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            //注册数据库基础操作和工作单元
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitWork), typeof(UnitWork));

            //注册app层(废弃->不使用此方法,修改采用配置文件)
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            ////防止单元测试时已经注入
            //if (services.All(u => u.ServiceType != typeof(ICacheContext)))
            //{
            //    services.AddScoped(typeof(ICacheContext), typeof(CacheContext));
            //}
            //if (services.All(u => u.ServiceType != typeof(IHttpContextAccessor)))
            //{
            //    services.AddScoped(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
            //}

            //将配置添加到ConfigurationBuilder
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
            //config.AddJsonFile来自Microsoft.Extensions.Configuration.Json
            //config.AddXmlFile来自Microsoft.Extensions.Configuration.Xml
            config.AddJsonFile("autofac.json");

            //用Autofac注册ConfigurationModule
            var module = new ConfigurationModule(config.Build());

            builder.RegisterModule(module);
            builder.Populate(services);
            _container = builder.Build();
            return _container;

        }
    }
}
