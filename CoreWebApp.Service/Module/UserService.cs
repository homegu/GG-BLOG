using CoreWebApp.Data.DBContext;
using CoreWebApp.Model;
using CoreWebApp.Model.Enum;
using CoreWebApp.Repository;
using CoreWebApp.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using CoreWebApp.Infrastructure;
using Newtonsoft.Json;
using CoreWebApp.Model.Dto;

namespace CoreWebApp.Service.Module
{
    public class UserService: ServiceCore<User>
    {
        public UserService(IRepository<User> repo, IUnitWork unitwork) :
            base(repo, unitwork)
        {

        }

        public Response<User> UserLogin(UserLoginDto userInfo) {

            if (userInfo.UserName.IsNullOrEmpty())
            {
                return Failed("账号不能为空");
            }
            if (userInfo.Password.IsNullOrEmpty())
            {
                return Failed("密码不能为空");
            }
            var lowerPwd = userInfo.Password.ToLower();//密码转小写
            var handlerPwd = MD5Helper.MD5WithSalt(lowerPwd);
            var user =  _repo.FindBy(x => x.UserName == userInfo.UserName);
            if (user == null)
            {
                return Failed("账号不存在");
            }
            if (user.PassWord != handlerPwd)
            {
                return Failed("密码错误");
            }
            var userJSONStr = JsonConvert.SerializeObject(user);
            void SetUserEntity() {
                ++user.LoginCount;
                user.LastLoginTime = DateTime.Now;
                user.RegisterIP = "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee请问请问去玩儿去玩儿我去二去玩儿去玩儿去玩儿去玩儿去玩儿我去额去玩儿去玩儿去玩儿";
            }
            SetUserEntity();
            using (var tran = _unitwork.BeginTransaction())
            {
                _repo.Update(user);
                tran.Commit();
            }
            return Success(JsonConvert.DeserializeObject<User>(userJSONStr));
        }
    }
}
