using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApp.Infrastructure;
using CoreWebApp.Service.Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        UserService userService;

        public UserController(UserService _service)
        {
            userService = _service;
        }

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <returns></returns>
        [Route("info")]
        [HttpGet]
        public Response GetInfo()
        {
            return userService.GetInfo(GetToken());
        }
    }
}