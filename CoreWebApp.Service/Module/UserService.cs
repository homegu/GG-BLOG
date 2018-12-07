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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace CoreWebApp.Service.Module
{
    public class UserService: ServiceCore<User>
    {
        public JwtSettings jwtSettings { get; set; }

        public UserService(IRepository<User> repo, IUnitWork unitwork) :
            base(repo, unitwork)
        {
            var jwtSetting = AutofacCore.GetFromFac<IOptions<JwtSettings>>();
            jwtSettings = jwtSetting.Value;
        }

        public Response<UserTokenModel> UserLogin(UserLoginDto userInfo) {

            if (userInfo.UserName.IsNullOrEmpty())
            {
                return Failed<UserTokenModel>("账号不能为空");
            }
            if (userInfo.Password.IsNullOrEmpty())
            {
                return Failed<UserTokenModel>("密码不能为空");
            }
            var lowerPwd = userInfo.Password.ToLower();//密码转小写
            var handlerPwd = MD5Helper.MD5WithSalt(lowerPwd);
            var user =  _repo.FindBy(x => x.UserName == userInfo.UserName);
            if (user == null)
            {
                return Failed<UserTokenModel>("账号不存在");
            }
            if (user.PassWord != handlerPwd)
            {
                return Failed<UserTokenModel>("密码错误");
            }
            if (!user.Enabled)
            {
                return Failed<UserTokenModel>("该用户被禁止登录");
            }
            var userJSONStr = JsonConvert.SerializeObject(user);
            void SetUserEntity() {
                ++user.LoginCount;
                var userTonkentModel = GetTonken(user);
                user.Token = userTonkentModel.Token;
                user.LastLoginTime = userTonkentModel.LastLoginTime;
            }
            SetUserEntity();
            using (var tran = _unitwork.BeginTransaction())
            {
                _repo.Update(user);
                tran.Commit();
            }
            return Success(JsonConvert.DeserializeObject<UserTokenModel>(userJSONStr));
        }


        public UserTokenModel GetTonken(User user)
        {
            var claims = new[]
                {
                   new Claim(ClaimTypes.Name, user.UserName),
                        //下边为Claim的默认配置
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    //这个就是过期时间，目前是过期100秒，可自定义，注意JWT有自己的缓冲过期时间
                    new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(100)).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Iss,jwtSettings.Issuer),
                    new Claim(JwtRegisteredClaimNames.Aud,jwtSettings.Audience),
                    //这个Role是官方UseAuthentication要要验证的Role，我们就不用手动设置Role这个属性了
                    new Claim(ClaimTypes.Role,user.Role),
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                //expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            //MemoryCacheHelper.AddMemoryCache(encodedJwt, new UserBaseViewModel {
            //    UserName = res.Result.UserName,
            //    Enabled = res.Result.Enabled,
            //    Id = res.Result.Id,
            //    LastLoginTime = res.Result.LastLoginTime
            //}, new TimeSpan(0,30,0), new TimeSpan(12,0,0));//将JWT字符串和tokenModel作为key和value存入缓存
            return new UserTokenModel
            {
                LastLoginTime = DateTime.Now,
                Token = encodedJwt,
                UserName = user.UserName,
            };
        }
    }
}
