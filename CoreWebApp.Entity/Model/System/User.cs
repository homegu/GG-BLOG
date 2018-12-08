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
    public class User: UserBaseModel
    {
        [MaxLength(32)]
        [Required(ErrorMessage = "密码不能为空")]
        public string PassWord { get; set; }
        public int LoginCount { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "注册IP不能为空")]
        public string RegisterIP { get; set; }
        [MaxLength(50)]
        public string LastLoginIP { get; set; }
    }

    public class UserBaseModel: Entity_Enabled
    {
        [MaxLength(32)]
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        public DateTime? LastLoginTime { get; set; }
        [MaxLength(1000)]
        public string Token { get; set; }
    }

    public class UserTokenModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
