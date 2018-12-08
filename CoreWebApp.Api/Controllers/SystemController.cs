using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreWebApp.Infrastructure;
using CoreWebApp.Infrastructure.Helper;
using CoreWebApp.Model;
using CoreWebApp.Model.Dto;
using CoreWebApp.Repository.Contract;
using CoreWebApp.Service;
using CoreWebApp.Service.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreWebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : BaseController
    {
        private UserService userService;

        public SystemController(UserService _service)
        {
            userService = _service;
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        //[Consumes("application/x-www-form-urlencoded")]
        public Response UserLogin(UserLoginDto user)
        {
            return userService.UserLogin(user);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [Route("Logout")]
        [Authorize]
        [HttpPost]
        public Response Logout(UserTokenModel userInfo)
        {
            return userService.Logout(userInfo);
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
