using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreWebApp.Repository;
using CoreWebApp.Repository.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using IContainer = Autofac.IContainer;

namespace CoreWebApp.Core
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

            //注册app层
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            ////防止单元测试时已经注入
            //if (services.All(u => u.ServiceType != typeof(ICacheContext)))
            //{
            //    services.AddScoped(typeof(ICacheContext), typeof(CacheContext));
            //}

            //if (services.All(u => u.ServiceType != typeof(IHttpContextAccessor)))
            //{
            //    services.AddScoped(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
            //}

            builder.Populate(services);

            _container = builder.Build();
            return _container;

        }
    }
}
