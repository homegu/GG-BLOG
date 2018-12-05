using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApp.Model
{
    [Table("sys_User")]
    public class User: UserBaseViewModel
    {
        [MaxLength(32)]
        [Required]
        public string PassWord { get; set; }
        public int LoginCount { get; set; }
        [MaxLength(50)]
        [Required]
        public string RegisterIP { get; set; }
        [MaxLength(50)]
        public string LastLoginIP { get; set; }
        public string Role { get; set; } = string.Empty;
    }

    public class UserBaseViewModel: Entity_Enabled
    {
        [MaxLength(32)]
        [Required]
        public string UserName { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }
}
