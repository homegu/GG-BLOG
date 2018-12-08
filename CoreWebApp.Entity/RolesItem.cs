using CoreWebApp.Model.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Model
{
    public class RolesItem
    {
        public static List<Roles> Items { get; private set; }

        static RolesItem()
        {
            Items = new List<Roles>();
            Items.Add(new Roles
            {
                 Id = 1,
                 RoleName = "Admin",
            });
        }
    }
}
