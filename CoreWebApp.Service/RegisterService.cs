using Autofac;
using CoreWebApp.Model;
using CoreWebApp.Repository;
using CoreWebApp.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutofacModule = Autofac.Module;

namespace CoreWebApp.Service
{
    public class RegisterService: AutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            //以下是几种注册方式

            //builder.RegisterAssemblyTypes(this.ThisAssembly)
            //.Where(type => type.Namespace.Equals("CoreWebApp.Service.Module")).InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
            //builder.RegisterAssemblyTypes(this.ThisAssembly);

            //builder.RegisterAssemblyModules<typeof(ServiceCore<>) as Type> (this.ThisAssembly);

            //builder.RegisterAssemblyTypes(this.ThisAssembly)
            //   .InNamespace("CoreWebApp.Service.Module")//类型在CoreWebApp.Service.Module命名空间中
            //   .AsImplementedInterfaces() //注册为其实现的服务接口
            //   .InstancePerLifetimeScope(); //注册模式为生命周期模式

            //builder.RegisterAssemblyTypes(this.ThisAssembly)
            //    .AsClosedTypesOf(typeof(IService))
            //    .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(this.ThisAssembly)
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(this.ThisAssembly)
              .Where(t => t.IsAssignableTo<IService>())
              .InstancePerLifetimeScope();
        }
    }
}
