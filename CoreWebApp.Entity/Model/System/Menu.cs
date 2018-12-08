using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Model.Model
{
    public class Menu
    {
        public string Path { get; set; }
        public string Component { get; set; }
        public string Redirect { get; set; }
        public string Name { get; set; }
        public Meta Meta { get; set; }
        public List<Menu> Children { get; set; } = new List<Menu>();
        public bool Hidden { get; set; }
    }

    public class Meta
    {
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
