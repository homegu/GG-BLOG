using CoreWebApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Service.Module
{
    public class RoutesService
    {
        public static List<Menu> Items { get; private set; }

        static RoutesService()
        {
            Items = new List<Menu>();
            Items.Add(new Menu
            {
                Path = "/login",
                Component = "@/views/login/index"
            });
            Items.Add(new Menu
            {
                Path = "/404",
                Component = "@/views/404"
            });
            Items.Add(new Menu
            {
                Path = "/",
                Component = "Layout",
                Name = "Dashboard",
                Redirect = "/dashboard",
                Hidden = true,
                Children = new List<Menu> {
                new Menu{
                     Path = "dashboard",
                     Component = "@/views/dashboard/index"
                }
            }});
            Items.Add(new Menu
            {
                Path = "/example",
                Component = "Layout",
                Name = "Example",
                Redirect = "/example/table",
                Hidden = true,
                Meta = new Meta {
                     Icon = "example",
                     Title = "Example",
                },
                Children = new List<Menu> {
                new Menu{
                     Path = "table",
                     Name = "table",
                     Component = "@/views/table/index",
                     Meta = new Meta{  Title = "Table",Icon = "table" }
                },
                new Menu{
                     Path = "tree",
                     Name = "tree",
                     Component = "@/views/tree/index",
                     Meta = new Meta{  Title = "Tree",Icon = "tree" }
                }
            }});
            Items.Add(new Menu
            {
                Path = "/form",
                Component = "Layout",
                Children = new List<Menu> {
                new Menu{
                     Path = "index",
                     Name  = "Form",
                     Component = "@/views/form/index",
                     Meta = new Meta{ Icon = "form", Title = "Form" }
                }
            }});
            Items.Add(new Menu
            {
                Path = "*",
                Redirect = "/404",
                Hidden = true,
            });
        }
    }
}
