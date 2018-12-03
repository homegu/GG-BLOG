using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoreWebApp.Model.Enum
{
    public enum UserLoginStatus
    {
        [Description("登陆成功")]
        success = 200,
        [Description("账号密码错误")]
        error = 201,
        [Description("验证码错误")]
        captcha_error = 202,
        [Description("登陆受限")]
        login_limit = 203,
        [Description("账号停用")]
        disable = 204
    }
}
