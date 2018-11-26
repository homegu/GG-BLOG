using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApp.Model
{
    [Table("sys_User")]
    public class User:Entity
    {
        [MaxLength(30)]
        public string UserName { get; set; }
        [MaxLength(30)]
        public string PassWord { get; set; }
        public bool Enabled { get; set; }
        public int LoginCount { get; set; }
        [MaxLength(50)]
        public string RegIP { get; set; }
        [MaxLength(50)]
        public string LastLoginIP { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
