using CoreWebApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Model.Dto
{
    public class UserInfoDto
    {
        public string UserName { get; set; }
        public List<Menu> Roles { get; set; } = RoutesItems.Items;
    }
}
