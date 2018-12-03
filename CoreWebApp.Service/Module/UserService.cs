using CoreWebApp.Data.DBContext;
using CoreWebApp.Model;
using CoreWebApp.Model.Enum;
using CoreWebApp.Repository;
using CoreWebApp.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using CoreWebApp.Infrastructure;

namespace CoreWebApp.Service.Module
{
    public class UserService: ServiceCore<User>
    {
        public UserService(IRepository<User> repo, IUnitWork unitwork) :
            base(repo, unitwork)
        {

        }

        public Response<User> UserLogin(string name,string pwd) {

            if (name.IsNullOrEmpty())
            {
                return Failed("账号不能为空");
            }
            if (pwd.IsNullOrEmpty())
            {
                return Failed("密码不能为空");
            }
            var lowerPwd = pwd.ToLower();//密码转小写
            var handlerPwd = MD5Helper.MD5WithSalt(lowerPwd);
            var user =  _repo.FindBy(x => x.UserName == name);
            if (user == null)
            {
                return Failed("账号不存在");
            }
            if (user.PassWord != handlerPwd)
            {
                return Failed("密码错误");
            }
            ++user.LoginCount;
            using (var tran = _unitwork.BeginTransaction())
            {
                _repo.Update(user);
                tran.Commit();
            }
            return Success(user);
        }
    }
}
